using System.Security.Cryptography;
using System.Text;
using CargasBrasilDB.Common;
using CargasBrasilDB.Mappings;
using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Repositories.Interfaces;
using CargasBrasilDB.Security;
using CargasBrasilDB.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace CargasBrasilDB.Services
{
    public class AuthService : IAuthService
    {
        private const string InvalidCredentials = "CPF ou senha inválidos.";

        private readonly IDriverRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly AdminSettings _admin;

        public AuthService(
            IDriverRepository repository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IOptions<AdminSettings> adminOptions)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _admin = adminOptions.Value;
        }

        public async Task<Result<LoginResponse>> LoginDriverAsync(LoginRequest request, CancellationToken ct = default)
        {
            var driver = await _repository.GetByCpfAsync(request.Cpf.Trim(), ct);

            if (driver is null)
            {
            
                _passwordHasher.Hash(request.Senha);
                return Result<LoginResponse>.Fail(ErrorType.Unauthorized, InvalidCredentials);
            }

            if (!_passwordHasher.Verify(request.Senha, driver.Senha))
                return Result<LoginResponse>.Fail(ErrorType.Unauthorized, InvalidCredentials);

         
            if (_passwordHasher.NeedsRehash(driver.Senha))
            {
                driver.Senha = _passwordHasher.Hash(request.Senha);
                await _repository.UpdateAsync(driver, ct);
            }

            var token = _tokenService.CreateDriverToken(driver);
            return Result<LoginResponse>.Ok(new LoginResponse(driver.ToResponse(), token), "Login realizado com sucesso.");
        }

        public Result<TokenResponse> LoginAdmin(AdminLoginRequest request)
        {
            var configured = !string.IsNullOrWhiteSpace(_admin.Usuario) && !string.IsNullOrWhiteSpace(_admin.SenhaHash);

            var userOk = configured && CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(request.Usuario), Encoding.UTF8.GetBytes(_admin.Usuario));

            // Só aceita o hash novo para o admin (nunca o SHA1 legado).
            var passwordOk = configured
                && !_passwordHasher.NeedsRehash(_admin.SenhaHash)
                && _passwordHasher.Verify(request.Senha, _admin.SenhaHash);

            if (!userOk || !passwordOk)
                return Result<TokenResponse>.Fail(ErrorType.Unauthorized, "Usuário ou senha inválidos.");

            return Result<TokenResponse>.Ok(_tokenService.CreateAdminToken(_admin.Usuario), "Login realizado com sucesso.");
        }
    }
}
