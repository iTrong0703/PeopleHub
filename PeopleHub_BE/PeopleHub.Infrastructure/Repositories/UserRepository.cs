using MediatR;
using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Domain.Entities;
using PeopleHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeopleHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
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
