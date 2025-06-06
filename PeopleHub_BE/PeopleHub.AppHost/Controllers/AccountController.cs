using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Features.Users.Commands.LoginUser;
using PeopleHub.Application.Features.Users.Commands.RegisterUser;
using PeopleHub.Application.Features.Users.DTOs;

namespace PeopleHub.AppHost.Controllers
{
    public class AccountController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;
        // Register an user
        // POST: {{localhost}}/api/account/register
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        // Login an user
        // POST: {{localhost}}/api/account/login
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
