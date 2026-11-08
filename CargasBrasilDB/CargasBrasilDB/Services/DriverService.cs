using CargasBrasilDB.Common;
using CargasBrasilDB.Mappings;
using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Repositories.Interfaces;
using CargasBrasilDB.Security;
using CargasBrasilDB.Services.Interfaces;

namespace CargasBrasilDB.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public DriverService(IDriverRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> RegisterAsync(DriverRegisterRequest request, CancellationToken ct = default)
        {
            if (await _repository.ExistsByCpfAsync(request.Cpf.Trim(), ct))
                return Result.Fail(ErrorType.Conflict, "Esse CPF já está em uso!");

            var driver = request.ToEntity();
            driver.Senha = _passwordHasher.Hash(request.Senha);

            await _repository.AddAsync(driver, ct);

            return Result.Ok("Cadastro realizado com sucesso! Aguarde alguns minutos que nossa equipe irá validar seus documentos.");
        }

        public async Task<Result> DeleteAsync(string cpf, string senha, CancellationToken ct = default)
        {
            var driver = await _repository.GetByCpfAsync(cpf, ct);

            if (driver is null || !_passwordHasher.Verify(senha, driver.Senha))
                return Result.Fail(ErrorType.Unauthorized, "Não foi possível excluir o motorista.");

            await _repository.DeleteAsync(driver, ct);
            return Result.Ok("Motorista excluído com sucesso.");
        }
    }
}
