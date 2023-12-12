using AutoMapper;
using AutoMapper.QueryableExtensions;
using Creations.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.Creations.Queries.GetCreations;
public record GetCreationsQuery : IRequest<IEnumerable<CreationDto>>
{
    public string BrickCode { get; init; }
}

public class GetCreationsQueryHandler : IRequestHandler<GetCreationsQuery, IEnumerable<CreationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCreationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IEnumerable<CreationDto>> Handle(GetCreationsQuery request, CancellationToken cancellationToken)
    {
        var brick = await _context.Bricks.FirstOrDefaultAsync(b => b.Code == request.BrickCode);

        return _context.Creations
            .Where(c => c.Bricks.Contains(brick))
            .ProjectTo<CreationDto>(_mapper.ConfigurationProvider);
    }
}
