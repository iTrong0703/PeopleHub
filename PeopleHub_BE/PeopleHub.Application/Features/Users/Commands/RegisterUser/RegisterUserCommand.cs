using MediatR;
using PeopleHub.Application.Features.Users.DTOs;

namespace PeopleHub.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(string Username, string Password) : IRequest<UserRegisterResponseDto>;
}
