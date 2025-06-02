using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public required string UserName { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        //public required string FullName { get; set; }
        //public string? Gender { get; set; }
        //public DateOnly DateOfBirth { get; set; }
        //public DateTimeOffset LastActive { get; set; } = DateTimeOffset.UtcNow;
        //public string? Position { get; set; }
        //public string? Department { get; set; }
        //public string? Location { get; set; }
        //public string? Bio { get; set; }
        //public string? AvatarUrl { get; set; }
    }
}
