﻿using Creations.Application.Common.Interfaces;
using MediatR;

namespace Creations.Application.Creations.Queries.GetCreations;
public record GetCreationsQuery : IRequest<IEnumerable<Creation>>
{
    public string BrickCode { get; init; }

    public GetCreationsQuery(string brickCode)
    {
        BrickCode = brickCode;
    }
}

public class GetCreationsQueryHandler : IRequestHandler<GetCreationsQuery, IEnumerable<Creation>>
{
    private readonly IApplicationDbContext _context;

    public GetCreationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Creation>> Handle(GetCreationsQuery request, CancellationToken cancellationToken)
    {
        var brick = _context.Bricks.FirstOrDefault(b => b.Code == request.BrickCode);

        return _context.Creations
            .Where(c => c.Bricks.Contains(brick))
            .Select(creation => new Creation
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