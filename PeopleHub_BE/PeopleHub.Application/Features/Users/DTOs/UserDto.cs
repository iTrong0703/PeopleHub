using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleHub.Application.Features.Users.DTOs
{
    public record UserResponseDto(int Id, string Username);
    public record CreateUserRequestDto(string Username, byte[] PasswordHash, byte[] PasswordSalt);
    public record UpdateUserRequestDto(string Username, string Password);
}
