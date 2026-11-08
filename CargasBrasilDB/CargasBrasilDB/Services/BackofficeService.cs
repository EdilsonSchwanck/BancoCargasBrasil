using CargasBrasilDB.Common;
using CargasBrasilDB.Mappings;
using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Repositories.Interfaces;
using CargasBrasilDB.Services.Interfaces;

namespace CargasBrasilDB.Services
{
    public class BackofficeService : IBackofficeService
    {
        private readonly IDriverRepository _repository;

        public BackofficeService(IDriverRepository repository) => _repository = repository;

        public async Task<Result<IReadOnlyList<PendingRegistrationResponse>>> GetPendingRegistrationsAsync(CancellationToken ct = default)
        {
            var drivers = await _repository.GetPendingApprovalAsync(ct);
            IReadOnlyList<PendingRegistrationResponse> data = drivers.Select(d => d.ToPendingResponse()).ToList();

            return Result<IReadOnlyList<PendingRegistrationResponse>>.Ok(data, "Consulta realizada com sucesso.");
        }

        public async Task<Result> SetApprovalAsync(ApproveRegistrationRequest request, CancellationToken ct = default)
        {
            var driver = await _repository.GetByCpfAsync(request.Cpf.Trim(), ct);
            if (driver is null)
                return Result.Fail(ErrorType.NotFound, "CPF não cadastrado.");

            driver.CadastroAprovado = request.CadastroAprovado;
            await _repository.UpdateAsync(driver, ct);

            return Result.Ok("Dados do usuário atualizados.");
        }
    }
}
