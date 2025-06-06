using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Domain.Entities;
using PeopleHub.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace PeopleHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> CreateUserAsync(CreateUserRequestDto request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                UserName = request.Username.ToLower(),
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt
            };
            await _context.Users.AddAsync(user, cancellationToken);
            return user;
        }

        public async Task<AppUser> FindByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower(), cancellationToken);
        }

        public async Task<PagedResult<AppUser>> GetUsersAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsNoTracking().OrderBy(u => u.Id);
            var totalCount = await query.CountAsync(cancellationToken);
            var users = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync(cancellationToken);
            return new PagedResult<AppUser>(
                users, 
                totalCount, 
                paginationParams.PageNumber, 
                paginationParams.PageSize);

        }
    }
}
