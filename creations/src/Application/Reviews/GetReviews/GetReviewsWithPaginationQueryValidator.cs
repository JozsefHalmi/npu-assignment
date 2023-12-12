using Reviews.Application.Reviews.Queries.GetReviews;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Creations.Application.Common.Interfaces;

namespace Creations.Application.Reviews.GetReviews;
public class GetReviewsWithPaginationQueryValidator : AbstractValidator<GetReviewsWithPaginationQuery>
{
    private readonly IApplicationDbContext _context;

    public GetReviewsWithPaginationQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.CreationId)
            .NotEmpty().WithMessage("Brick code is required.")
            .MustAsync(Exist).WithMessage("The specified creation must exist.");
    }

    public async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        return await _context.Creations
            .AnyAsync(l => l.Id == id, cancellationToken);
    }
}
