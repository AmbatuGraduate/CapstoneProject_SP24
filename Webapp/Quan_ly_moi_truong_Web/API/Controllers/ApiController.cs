using API.Common.Http;
using ErrorOr;
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
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
            if (errors.Count is 0)
            {
                return Problem();
            }

            // Having bug, don't show detail of list error
            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                return ValidateProblem(errors);
            }

            return Problem(errors[0]);
        }

        private IActionResult Problem(Error firstError)
        {
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }

        private IActionResult ValidateProblem(List<Error> errors)
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