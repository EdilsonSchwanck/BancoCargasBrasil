using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Security;
using CargasBrasilDB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargasBrasilDB.Controllers
{
  
    [Route("api")]
    [Authorize(Roles = Roles.Admin)]
    public class BackofficeController : ApiControllerBase
    {
        private readonly IBackofficeService _backofficeService;

        public BackofficeController(IBackofficeService backofficeService) => _backofficeService = backofficeService;

        [HttpGet("ConsultarFormulario")]
        public async Task<IActionResult> GetPending(CancellationToken ct)
            => ToActionResult(await _backofficeService.GetPendingRegistrationsAsync(ct));

        [HttpPost("AprovarCadastro")]
        public async Task<IActionResult> Approve([FromBody] ApproveRegistrationRequest request, CancellationToken ct)
            => ToActionResult(await _backofficeService.SetApprovalAsync(request, ct));
    }
}
