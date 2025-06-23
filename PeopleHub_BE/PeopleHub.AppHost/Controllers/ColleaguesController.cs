using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Colleagues.Dtos.Responses;
using PeopleHub.Application.Features.Colleagues.Queries.GetColleagues;

namespace PeopleHub.AppHost.Controllers
{
    [Authorize]
    public class ColleaguesController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;
        // Get colleagues by page
        // GET: {{localhost}}/api/colleagues
        [HttpGet]
        public async Task<ActionResult<PagedResult<ColleaguesResponseDto>>> GetColleagues(
            [FromQuery] GetColleaguesQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        // Get user by id
        // GET: {{localhost}}/api/users/{{id}}
        //[HttpGet("id/{id}")]
        //public async Task<ActionResult<PagedResult<UserResponseDto>>> GetUserById(
        //    [FromQuery] GetUsersQuery query,
        //    CancellationToken cancellationToken = default)
        //{
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return Ok(result);
        //}

        // Get user by Username
        // GET: {{localhost}}/api/users/{{username}}
        //[HttpGet("username/{username}")]
        //public async Task<ActionResult<PagedResult<UserResponseDto>>> GetUserByUsername(
        //    [FromQuery] GetUsersQuery query,
        //    CancellationToken cancellationToken = default)
        //{
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return Ok(result);
        //}
    }
}
