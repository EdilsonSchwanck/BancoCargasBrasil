namespace CargasBrasilDB.Domin.DTOs.LoginDTO
{
    public class LoginResponse
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Cep { get; set; }
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? FotoHabilitacao { get; set; }
        public Boolean CadastroAprovado { get; set; } = false;
    }
}
