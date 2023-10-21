using System.ComponentModel.Design;
using Creations.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.Reviews.Commands.CreateReview;
public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateReviewCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Text)
            .NotEmpty();

        RuleFor(v => v.UniquenessScore)
            .GreaterThan(0)
            .LessThanOrEqualTo(10);

        RuleFor(v => v.CreativityScore)
           .GreaterThan(0)
           .LessThanOrEqualTo(10);

        RuleFor(v => v.CustomerId)
            .MustAsync(CustomerExists).WithMessage("The specified customer does not exist.");

        RuleFor(v => new ReviewExternalIds { CreationId = v.CreationId, CustomerId = v.CustomerId })
            .MustAsync(BeUnique).WithMessage("The review exists already.");

    }

    internal async Task<bool> CustomerExists(int customerId, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AnyAsync(l => l.Id == customerId, cancellationToken);
    }

    internal async Task<bool> CreationExists(int creationId, CancellationToken cancellationToken)
    {
        return await _context.Creations
            .AnyAsync(l => l.Id == creationId, cancellationToken);
    }

    internal async Task<bool> BeUnique(ReviewExternalIds reviewExternalIds, CancellationToken cancellationToken)
    {
        return !await _context.Reviews
            .AnyAsync(r => r.Customer.Id == reviewExternalIds.CustomerId && r.Creation.Id == reviewExternalIds.CreationId, cancellationToken);
    }

    internal class ReviewExternalIds
    {
        public int CustomerId { get; set; }
        public int CreationId { get; set; }
    }
}
