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

    public GetCreationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<CreationDto>> Handle(GetCreationsQuery request, CancellationToken cancellationToken)
    {
        var brick = await _context.Bricks.FirstOrDefaultAsync(b => b.Code == request.BrickCode);

        return _context.Creations
            .Where(c => c.Bricks.Contains(brick))
            .Select(creation => new CreationDto
            {
                CreatedBy = creation.CreatedBy,
                CreatedDate = creation.Created,
                //CreativityScore = creation.Created TODO
                //UniquenessScore TODO
                ImagePath = creation.ImagePath,
                ThumbnailPath = creation.ThumbnailPath,
                Description = creation.Description
            });
    }
}
