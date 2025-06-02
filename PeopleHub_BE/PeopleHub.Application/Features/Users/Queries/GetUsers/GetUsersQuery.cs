using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;

namespace PeopleHub.Application.Features.Users.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<PagedResult<UserResponseDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchTerm { get; init; }
        public string? SortBy { get; init; }
        public bool SortDescending { get; init; } = false;
    }
}
