using Creations.Application.Common.Interfaces;
using Creations.Domain.Entities;
using Creations.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reviews.Domain.Events;

namespace Creations.Application.Reviews.Commands.CreateReview;
public record CreateReviewCommand : IRequest<int>
{
    public int CustomerId { get; init; }
    public int CreationId { get; init; }
    public int UniquenessScore { get; init; }
    public int CreativityScore { get; init; }
    public string? Text { get; init; }
}

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FirstAsync(c => c.Id == request.CustomerId);
        var creation = await _context.Creations.FirstAsync(c => c.Id == request.CreationId);

        var entity = new Review
        {
            CreativityScore = request.CreativityScore,
            UniquenessScore = request.UniquenessScore,
            Creation = creation,
            Customer = customer,
            Text = request.Text
        };

        entity.AddDomainEvent(new ReviewCreatedEvent(entity));

        _context.Reviews.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
