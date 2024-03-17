namespace CargasBrasilDB.Domin.DTOs.CadastroAprovadoDTO
{
    public class CadastroResponseDTO
    {
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? FotoHabilitacao { get; set; }
        public Boolean CadastroAprovado { get; set; } = false;
        public string? PlacaVeiculo { get; set; }
        public string? FotoDocumentoVeiculo { get; set; }
    }
}
