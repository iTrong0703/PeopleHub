namespace PeopleHub.Application.Features.Colleagues.Dtos.Responses
{
    public class ColleaguesResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? Location { get; set; }  
        public string? AvatarUrl { get; set; }
    }
}
