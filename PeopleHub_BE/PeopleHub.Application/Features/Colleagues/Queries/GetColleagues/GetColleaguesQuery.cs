using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Colleagues.Dtos.Responses;

namespace PeopleHub.Application.Features.Colleagues.Queries.GetColleagues
{
    public class GetColleaguesQuery : IRequest<PagedResult<ColleaguesResponseDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchTerm { get; init; }
        public string? SortBy { get; init; }
        public bool SortDescending { get; init; } = false;
    }
}
