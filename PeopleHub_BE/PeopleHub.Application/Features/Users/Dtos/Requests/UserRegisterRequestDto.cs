namespace PeopleHub.Application.Features.Users.Dtos.Requests
{
    public class UserRegisterRequestDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;

        public string FullName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
    }
}
