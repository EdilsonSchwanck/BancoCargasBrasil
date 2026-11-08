using System.ComponentModel.DataAnnotations;
using CargasBrasilDB.Common.Validation;

namespace CargasBrasilDB.Models.DTOs
{
    public class DriverRegisterRequest
    {
        [Required, StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        [Required, Cpf]
        public string Cpf { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(80)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Telefone { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Cep { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Endereco { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Numero { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Cidade { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Estado { get; set; } = string.Empty;

        [Required]
        public string FotoHabilitacao { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string ModeloVeiculo { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string PlacaVeiculo { get; set; } = string.Empty;

        [Required]
        public string FotoDocumentoVeiculo { get; set; } = string.Empty;

        [Required, StringLength(128, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 128 caracteres.")]
        public string Senha { get; set; } = string.Empty;
    }
}
