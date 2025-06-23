using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Features.Users.Commands.LoginUser;
using PeopleHub.Application.Features.Users.Commands.RegisterUser;
using PeopleHub.Application.Features.Users.Dtos.Requests;
using PeopleHub.Application.Features.Users.Dtos.Responses;

namespace PeopleHub.AppHost.Controllers
{
    public class AccountController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;
        // Register an user
        // POST: {{localhost}}/api/account/register
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponseDto>> Register([FromBody] UserRegisterRequestDto dto)
        {
            var command = new RegisterUserCommand { RegisterRequest = dto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        // Login an user
        // POST: {{localhost}}/api/account/login
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> Login([FromBody] UserLoginRequestDto dto)
        {
            var command = new LoginUserCommand { LoginRequest = dto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
