using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Features.Users.Queries.GetUsers;

namespace PeopleHub.AppHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<PagedResult<UserResponseDto>>> GetUsers(
            [FromQuery] GetUsersQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
