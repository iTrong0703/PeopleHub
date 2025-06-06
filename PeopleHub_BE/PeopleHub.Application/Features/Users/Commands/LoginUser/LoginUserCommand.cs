using PeopleHub.Application.Features.Users.DTOs;

namespace PeopleHub.Application.Features.Users.Commands.LoginUser
{
    public record LoginUserCommand(string Username, string Password) : IRequest<UserLoginResponseDto>;
}
