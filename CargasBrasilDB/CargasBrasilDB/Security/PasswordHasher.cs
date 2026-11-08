using System.Security.Cryptography;
using System.Text;

namespace CargasBrasilDB.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string storedHash);
        bool NeedsRehash(string storedHash);
    }


    public sealed class Pbkdf2PasswordHasher : IPasswordHasher
    {
        private const string Version = "v1";
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 210_000; 
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return $"{Version}.{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash)) return false;

            var parts = storedHash.Split('.');
            if (parts.Length == 4 && parts[0] == Version && int.TryParse(parts[1], out var iterations) && iterations > 0)
            {
                try
                {
                    var salt = Convert.FromBase64String(parts[2]);
                    var expected = Convert.FromBase64String(parts[3]);
                    var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, expected.Length);
                    return CryptographicOperations.FixedTimeEquals(actual, expected);
                }
                catch (FormatException)
                {
                    return false;
                }
            }

            return VerifyLegacySha1(password, storedHash);
        }

        public bool NeedsRehash(string storedHash) => !storedHash.StartsWith(Version + ".", StringComparison.Ordinal);

        /// <summary>Formato antigo: SHA1 sem salt, com os bytes concatenados em decimal.</summary>
        private static bool VerifyLegacySha1(string password, string storedHash)
        {
            var data = SHA1.HashData(Encoding.UTF8.GetBytes(password));
            var legacy = string.Concat(data.Select(b => b.ToString()));
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(legacy), Encoding.UTF8.GetBytes(storedHash));
        }
    }
}
