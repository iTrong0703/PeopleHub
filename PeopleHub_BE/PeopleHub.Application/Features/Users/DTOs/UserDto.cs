using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleHub.Application.Features.Users.DTOs
{
    public record UserResponseDto(int Id, string UserName, string Email);
    public record CreateUserRequestDto(string UserName, string Email);
    public record UpdateUserRequestDto(string UserName, string Email);
}
