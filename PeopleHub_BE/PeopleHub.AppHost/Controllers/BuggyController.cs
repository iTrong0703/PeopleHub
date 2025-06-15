using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Exceptions;

namespace PeopleHub.AppHost.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            throw new UnauthorizedAccessException("You are not authorized to access this resource.");
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            throw new NotFoundException("The requested resource was not found.");
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            throw new Exception("This is a simulated server error.");
        }

        [HttpGet("validation-error")]
        public ActionResult<string> GetValidationError()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Username", "The Username field is required."),
                new ValidationFailure("Username", "The Username must be at least 6 characters."),
                new ValidationFailure("Password", "The Password field is required.")
            };

            throw new ValidationException(failures);
        }
    }
}
