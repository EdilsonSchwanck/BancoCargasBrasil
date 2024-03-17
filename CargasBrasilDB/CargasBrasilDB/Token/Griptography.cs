using System.Security.Cryptography;
using System.Text;

namespace CargasBrasilDB.Token
{
    public class Griptography
    {
        public static string Hash(string? senha)
        {
            SHA1 sha1 = SHA1.Create(); byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(senha));
            StringBuilder returnValue = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            return returnValue.ToString();

        }
    }
}
