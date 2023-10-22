using AutoMapper;
using AutoMapper.QueryableExtensions;
using Creations.Application.Common.Interfaces;
using Creations.Application.Common.Mappings;
using Creations.Application.Common.Models;
using Creations.Application.Reviews.GetReviews;
using MediatR;

namespace Reviews.Application.Reviews.Queries.GetReviews;
public record GetReviewsWithPaginationQuery : IRequest<PaginatedList<ReviewDto>>
{
    public int CreationId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetReviewsWithPaginationQueryHandler : IRequestHandler<GetReviewsWithPaginationQuery, PaginatedList<ReviewDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReviewsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<PaginatedList<ReviewDto>> Handle(GetReviewsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .Where(r => r.Creation.Id == request.CreationId)
            .OrderByDescending(x => x.Created)
            .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
