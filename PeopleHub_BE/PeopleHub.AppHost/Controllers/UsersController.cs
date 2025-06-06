using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Features.Users.Queries.GetUsers;

namespace PeopleHub.AppHost.Controllers
{
    
    public class UsersController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;
        // Get users by page
        // GET: {{localhost}}/api/users
        [Authorize]
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
