using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public required string UserName { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public required string Email { get; set; }

        // navigation
        public UserProfile Profile { get; set; } = default!;
    }
}
