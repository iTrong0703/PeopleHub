using PeopleHub.Application.Features.Users.Dtos.Requests;
using PeopleHub.Application.Features.Users.Dtos.Responses;

namespace PeopleHub.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserLoginResponseDto>
    {
        public UserLoginRequestDto LoginRequest { get; set; } = default!;
    }
}
