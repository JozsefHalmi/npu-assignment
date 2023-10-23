using Creations.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.Creations.Queries.GetCreations;
public class GetCreationsQueryValidator : AbstractValidator<GetCreationsQuery>
{
    private readonly IApplicationDbContext _context;

    public GetCreationsQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.BrickCode)
            .NotEmpty().WithMessage("Brick code is required.")
            .MustAsync(Exist).WithMessage("The specified brick must exist.");
    }

    public async Task<bool> Exist(string brickCode, CancellationToken cancellationToken)
    {
        return await _context.Bricks
            .AnyAsync(l => l.Code == brickCode, cancellationToken);
    }
}
