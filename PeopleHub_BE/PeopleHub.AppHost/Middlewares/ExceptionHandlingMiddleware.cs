using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PeopleHub.Application.Common.Exceptions;
using System.Net;

namespace PeopleHub.AppHost.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next;
            _problemDetailsFactory = problemDetailsFactory;
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
                    foreach (var error in validationException.Errors)
                    {
                        problem.Extensions[error.Key] = error.Value;
                    }
                    break;
                //case NotFoundException notFoundException:
                //    problem.Status = (int)HttpStatusCode.NotFound;
                //    problem.Title = "Not Found";
                //    problem.Detail = notFoundException.Message;
                //    break;
                default:
                    problem.Status = (int)HttpStatusCode.InternalServerError;
                    problem.Title = "Internal Server Error";
                    problem.Detail = exception?.Message ?? "An unexpected error occurred.";
                    break;
            }

            problem.Type = "https://example.com/problems";

            var result = new ObjectResult(problem)
            {
                StatusCode = problem.Status
            };
            httpContext.Response.ContentType = "application/problem+json";
            await result.ExecuteResultAsync(new ActionContext(httpContext, new RouteData(), new ActionDescriptor()));
        }
    }
}
