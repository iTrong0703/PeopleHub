namespace PeopleHub.Application.Features.Users.Dtos.Requests
{
    public class UserLoginRequestDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
