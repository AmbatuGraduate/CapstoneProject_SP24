using API.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers
{

    /// <summary>
    /// Base class for return problem when get error in validation
    /// </summary>
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {

            // Having bug, don't show detail of list error
            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

        private IActionResult Problem(Error firstError)
        {
            HttpContext.Items[HttpContextItemKeys.Errors] = firstError;
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }
        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                        error.Code,
                        error.Description
                    );
            }

            return ValidationProblem(modelStateDictionary);
        }

    }
}
