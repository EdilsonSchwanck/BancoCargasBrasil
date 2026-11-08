using System.ComponentModel.DataAnnotations;

namespace CargasBrasilDB.Models.DTOs
{
    public class ApproveRegistrationRequest
    {
        [Required]
        public string Cpf { get; set; } = string.Empty;

        public bool CadastroAprovado { get; set; }
    }

    public class PendingRegistrationResponse
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string FotoHabilitacao { get; set; } = string.Empty;
        public bool CadastroAprovado { get; set; }
        public string PlacaVeiculo { get; set; } = string.Empty;
        public string FotoDocumentoVeiculo { get; set; } = string.Empty;
    }
}
