using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Colleagues.Dtos.Responses;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Domain.Entities;
using PeopleHub.Infrastructure.Data;

namespace PeopleHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Profile)
                    .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);
        }

        public async Task<PagedResult<AppUser?>> GetUsersAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
        {
            var query = _context.Users
                .Include(u => u.Profile)
                .ThenInclude(p => p.Photos)
                .AsNoTracking()
                .OrderBy(u => u.Id);
            var totalCount = await query.CountAsync(cancellationToken);
            var users = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync(cancellationToken);
            return new PagedResult<AppUser?>(
                users,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize);
        }

        public async Task<AppUser?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FindAsync(id, cancellationToken);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<AppUser> CreateUserAsync(AppUser user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            return user;
        }

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username, cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<PagedResult<ColleaguesResponseDto>> GetColleaguesAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
        {
            var query = _context.UserProfiles
                .Include(p => p.Photos)
                .AsNoTracking();
            var totalCount = await query.CountAsync(cancellationToken);
            
            var colleagues = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(p => new ColleaguesResponseDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    Position = p.Position,
                    Department = p.Department,
                    Location = p.Location,
                    AvatarUrl = p.Photos.FirstOrDefault(photo => photo.IsMain)!.Url
                })
                .ToListAsync(cancellationToken);
            return new PagedResult<ColleaguesResponseDto>(
                colleagues,
                totalCount,
                paginationParams.PageNumber,
                paginationParams.PageSize);
        }
    }
}
