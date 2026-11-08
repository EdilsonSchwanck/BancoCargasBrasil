namespace CargasBrasilDB.Models.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string FotoHabilitacao { get; set; } = string.Empty;
        public bool CadastroAprovado { get; set; }
        public string Senha { get; set; } = string.Empty;
        public string ModeloVeiculo { get; set; } = string.Empty;
        public string PlacaVeiculo { get; set; } = string.Empty;
        public string FotoDocumentoVeiculo { get; set; } = string.Empty;
    }
}
