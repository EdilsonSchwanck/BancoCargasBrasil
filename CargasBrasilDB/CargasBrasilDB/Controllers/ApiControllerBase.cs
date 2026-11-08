using CargasBrasilDB.Common;
using Microsoft.AspNetCore.Mvc;

namespace CargasBrasilDB.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult ToActionResult(Result result)
        {
            var body = new ApiResponse(result.Success, result.Message, result.Payload);

            return result.Error switch
            {
                ErrorType.None => Ok(body),
                ErrorType.Validation => BadRequest(body),
                ErrorType.NotFound => NotFound(body),
                ErrorType.Conflict => Conflict(body),
                ErrorType.Unauthorized => Unauthorized(body),
                _ => StatusCode(StatusCodes.Status500InternalServerError, body)
            };
        }
    }
}
