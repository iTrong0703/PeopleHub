using PeopleHub.Application.Features.Users.Dtos.Requests;
using PeopleHub.Application.Features.Users.Dtos.Responses;

namespace PeopleHub.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserRegisterResponseDto>
    {
        public UserRegisterRequestDto RegisterRequest { get; init; } = default!;
    }
}
