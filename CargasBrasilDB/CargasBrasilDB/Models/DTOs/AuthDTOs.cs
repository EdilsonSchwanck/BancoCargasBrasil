using System.ComponentModel.DataAnnotations;

namespace CargasBrasilDB.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }

    public class AdminLoginRequest
    {
        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }

   
    public class DeleteAccountRequest
    {
        [Required]
        public string Senha { get; set; } = string.Empty;
    }

    public record TokenResponse(string AccessToken, DateTime Expiration);

    public record LoginResponse(DriverResponse Motorista, TokenResponse Token);
}
