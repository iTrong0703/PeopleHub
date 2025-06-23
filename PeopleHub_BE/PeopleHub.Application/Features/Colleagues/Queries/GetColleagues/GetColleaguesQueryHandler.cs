using AutoMapper;
using PeopleHub.Application.Common.Models;
using PeopleHub.Application.Features.Colleagues.Dtos.Responses;
using PeopleHub.Application.Interfaces;

namespace PeopleHub.Application.Features.Colleagues.Queries.GetColleagues
{
    public class GetColleaguesQueryHandler : IRequestHandler<GetColleaguesQuery, PagedResult<ColleaguesResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetColleaguesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<ColleaguesResponseDto>> Handle(GetColleaguesQuery request, CancellationToken cancellationToken)
        {
            var paginationParams = new PaginationParams
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending
            };
            var pagedUsers = await _unitOfWork.Users.GetColleaguesAsync(paginationParams, cancellationToken);
            var usersDto = _mapper.Map<List<ColleaguesResponseDto>>(pagedUsers.Items);
            return new PagedResult<ColleaguesResponseDto>(
                usersDto,
                pagedUsers.TotalCount,
                pagedUsers.PageNumber,
                pagedUsers.PageSize
            );
        }
    }
}
