using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
