using System.ComponentModel.DataAnnotations;

namespace CargasBrasilDB.Security
{
    public static class Roles
    {
        public const string Motorista = "Motorista";
        public const string Admin = "Admin";
    }

    public static class AppClaims
    {
        public const string Cpf = "cpf";
    }

    public static class RateLimitPolicies
    {
        public const string Auth = "auth";
    }


    public class JwtSettings
    {
        public const string SectionName = "Jwt";

        [Required, MinLength(32, ErrorMessage = "Jwt:Key precisa ter pelo menos 32 caracteres.")]
        public string Key { get; set; } = string.Empty;

        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;

        [Range(1, 1440)]
        public int ExpirationMinutes { get; set; } = 60;
    }

    
    public class AdminSettings
    {
        public const string SectionName = "Admin";

        public string Usuario { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
    }
}
