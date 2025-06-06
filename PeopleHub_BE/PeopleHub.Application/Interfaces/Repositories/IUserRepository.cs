﻿using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleHub.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<PagedResult<AppUser>> GetUsersAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task<AppUser> CreateUserAsync(CreateUserRequestDto request, CancellationToken cancellationToken);
        Task<AppUser> FindByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}
