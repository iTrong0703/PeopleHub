namespace PeopleHub.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string FullName { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? Location { get; set; }
        public string? Bio { get; set; }
        public DateTimeOffset LastActive { get; set; } = DateTimeOffset.UtcNow;

        // Navigation
        public int AppUserId { get; set; }
        public AppUser User { get; set; } = default!;
        public List<Photo> Photos { get; set; } = [];
    }
}
