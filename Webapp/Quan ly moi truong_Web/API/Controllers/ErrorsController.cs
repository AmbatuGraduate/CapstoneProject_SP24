
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Return error if some exception occur
    /// </summary>
    public class ErrorsController : ControllerBase
    {
        [HttpPost("/error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, msg) = exception switch
            {
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")

            };

            return Problem(statusCode: statusCode, title: msg);
        }

    }
}
