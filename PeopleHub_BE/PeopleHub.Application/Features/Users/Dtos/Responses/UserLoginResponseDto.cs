namespace PeopleHub.Application.Features.Users.Dtos.Responses
{
    public record UserLoginResponseDto(
        string Username,
        string FullName,
        string PhotoUrl,
        string Token
    );
}
