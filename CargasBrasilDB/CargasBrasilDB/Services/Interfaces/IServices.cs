using CargasBrasilDB.Common;
using CargasBrasilDB.Models.DTOs;

namespace CargasBrasilDB.Services.Interfaces
{
    public interface IDriverService
    {
        Task<Result> RegisterAsync(DriverRegisterRequest request, CancellationToken ct = default);
        Task<Result> DeleteAsync(string cpf, string senha, CancellationToken ct = default);
    }

    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginDriverAsync(LoginRequest request, CancellationToken ct = default);
        Result<TokenResponse> LoginAdmin(AdminLoginRequest request);
    }

    public interface IBackofficeService
    {
        Task<Result<IReadOnlyList<PendingRegistrationResponse>>> GetPendingRegistrationsAsync(CancellationToken ct = default);
        Task<Result> SetApprovalAsync(ApproveRegistrationRequest request, CancellationToken ct = default);
    }
}
