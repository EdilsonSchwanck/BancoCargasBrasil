using System.ComponentModel.DataAnnotations;

namespace CargasBrasilDB.Domin.DTOs.TokenDTOs
{
    public class TokenDTO
    {
        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        public string Cpf { get; set; }
    }
}
