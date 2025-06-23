using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Colleagues.Dtos.Responses;
using PeopleHub.Application.Features.Users.Dtos;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<PagedResult<AppUser?>> GetUsersAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task<PagedResult<ColleaguesResponseDto>> GetColleaguesAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task<AppUser?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
        Task<AppUser> CreateUserAsync(AppUser user, CancellationToken cancellationToken);
        Task<AppUser?> FindByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
    }
}
