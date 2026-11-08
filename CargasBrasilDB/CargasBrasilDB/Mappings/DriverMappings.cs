using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Models.Entities;

namespace CargasBrasilDB.Mappings
{
 
    public static class DriverMappings
    {
        public static Driver ToEntity(this DriverRegisterRequest r) => new()
        {
            Nome = r.Nome.Trim(),
            Cpf = r.Cpf.Trim(),
            Email = r.Email.Trim(),
            Telefone = r.Telefone.Trim(),
            Cep = r.Cep.Trim(),
            Endereco = r.Endereco.Trim(),
            Numero = r.Numero.Trim(),
            Cidade = r.Cidade.Trim(),
            Estado = r.Estado.Trim(),
            FotoHabilitacao = r.FotoHabilitacao,
            ModeloVeiculo = r.ModeloVeiculo.Trim(),
            PlacaVeiculo = r.PlacaVeiculo.Trim(),
            FotoDocumentoVeiculo = r.FotoDocumentoVeiculo,
            CadastroAprovado = false
        };

        public static DriverResponse ToResponse(this Driver d) => new()
        {
            Nome = d.Nome,
            Cpf = d.Cpf,
            Email = d.Email,
            Telefone = d.Telefone,
            Cep = d.Cep,
            Endereco = d.Endereco,
            Numero = d.Numero,
            Cidade = d.Cidade,
            Estado = d.Estado,
            FotoHabilitacao = d.FotoHabilitacao,
            CadastroAprovado = d.CadastroAprovado
        };

        public static PendingRegistrationResponse ToPendingResponse(this Driver d) => new()
        {
            Nome = d.Nome,
            Cpf = d.Cpf,
            Email = d.Email,
            Telefone = d.Telefone,
            FotoHabilitacao = d.FotoHabilitacao,
            CadastroAprovado = d.CadastroAprovado,
            PlacaVeiculo = d.PlacaVeiculo,
            FotoDocumentoVeiculo = d.FotoDocumentoVeiculo
        };
    }
}
