using System.Security.Claims;
using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Security;
using CargasBrasilDB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CargasBrasilDB.Controllers
{
    [Route("api")]
    public class DriverController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService) => _driverService = driverService;

        [HttpPost("CadastrarMotorista")]
        [EnableRateLimiting(RateLimitPolicies.Auth)]
        public async Task<IActionResult> Register([FromBody] DriverRegisterRequest request, CancellationToken ct)
            => ToActionResult(await _driverService.RegisterAsync(request, ct));

       
        [HttpDelete("ExcluirUsuario")]
        [Authorize(Roles = Roles.Motorista)]
        [EnableRateLimiting(RateLimitPolicies.Auth)]
        public async Task<IActionResult> Delete([FromBody] DeleteAccountRequest request, CancellationToken ct)
        {
            var cpf = User.FindFirstValue(AppClaims.Cpf);
            if (string.IsNullOrEmpty(cpf)) return Forbid();

            return ToActionResult(await _driverService.DeleteAsync(cpf, request.Senha, ct));
        }
    }
}
