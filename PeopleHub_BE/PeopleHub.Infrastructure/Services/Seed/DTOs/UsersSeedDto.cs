namespace PeopleHub.Infrastructure.Services.Seed.DTOs
{
    public record AppUserSeedDto(
        string UserName,
        string DateOfBirth,
        string FullName,
        string Gender,
        string Email,
        string Position,
        string Department,
        string Location,
        string Bio,
        string LastActive,
        List<PhotoSeedDto> Photos
    );
    public record PhotoSeedDto(
        string Url,
        bool IsMain
    );
}
