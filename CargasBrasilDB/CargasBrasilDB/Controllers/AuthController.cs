using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Security;
using CargasBrasilDB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CargasBrasilDB.Controllers
{
    [Route("api")]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

      
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
            => ToActionResult(await _authService.LoginDriverAsync(request, ct));

       
        [HttpPost("admin/Login")]
        public IActionResult AdminLogin([FromBody] AdminLoginRequest request)
            => ToActionResult(_authService.LoginAdmin(request));
    }
}
