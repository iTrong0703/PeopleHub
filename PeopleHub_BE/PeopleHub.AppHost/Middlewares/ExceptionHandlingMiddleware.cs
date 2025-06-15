using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PeopleHub.Application.Common.Exceptions;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PeopleHub.AppHost.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var problem = new ProblemDetails()
            {
                Instance = httpContext.Request.Path
            };

            switch (exception)
            {
                case ValidationException validationException:
                    problem.Status = (int)HttpStatusCode.BadRequest;
                    problem.Title = "Validation Failed";
                    problem.Detail = validationException.Message;
                    var errors = new Dictionary<string, string[]>();
                    foreach (var error in validationException.Errors)
                    {
                        errors[error.Key] = error.Value;
                    }
                    problem.Extensions["errors"] = errors;
                    break;
                case UnauthorizedAccessException unauthorizedException:
                    problem.Status = (int)HttpStatusCode.Unauthorized;
                    problem.Title = "Unauthorized";
                    problem.Detail = unauthorizedException.Message;
                    break;
                case NotFoundException notFoundException:
                    problem.Status = (int)HttpStatusCode.NotFound;
                    problem.Title = "Not Found";
                    problem.Detail = notFoundException.Message;
                    break;
                default:
                    problem.Status = (int)HttpStatusCode.InternalServerError;
                    problem.Title = "Internal Server Error";
                    problem.Detail = exception?.Message ?? "An unexpected error occurred.";
                    break;
            }

            problem.Type = "https://example.com/problems";

            if (_env.IsDevelopment())
            {
                problem.Extensions["stackTrace"] = exception.StackTrace;
            }

            var result = new ObjectResult(problem)
            {
                StatusCode = problem.Status
            };
            httpContext.Response.ContentType = "application/problem+json";
            await result.ExecuteResultAsync(new ActionContext(httpContext, new RouteData(), new ActionDescriptor()));
        }
    }
}
