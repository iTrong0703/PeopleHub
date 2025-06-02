using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces;

namespace PeopleHub.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResult<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var paginationParams = new PaginationParams
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending
            };
            var pagedUsers = await _unitOfWork.Users.GetUsersAsync(paginationParams, cancellationToken);
            var userDtos = pagedUsers.Items.Select(u => new UserResponseDto(u.Id, u.UserName)).ToList();
            return new PagedResult<UserResponseDto>(userDtos, pagedUsers.TotalCount, pagedUsers.PageNumber, pagedUsers.PageSize);
        }
    }
}
