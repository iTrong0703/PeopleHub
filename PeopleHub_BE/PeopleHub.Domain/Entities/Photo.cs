using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Entities
{
    public class Photo : BaseEntity
    {
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        // navigation properties
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; } = default!;
    }
}