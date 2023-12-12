using Creations.Application.Common.Interfaces;
using Creations.Domain.Entities;
using Creations.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.Creations.Commands.CreateCreation;
public record CreateCreationCommand : IRequest<int>
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string ThumbnailPath { get; init; }
    public string ImagePath { get; init; }
    public int CustomerId { get; init; }
    public IEnumerable<string> BrickCodes { get; init; }
}

public class CreateCreationCommandHandler : IRequestHandler<CreateCreationCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCreationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCreationCommand request, CancellationToken cancellationToken)
    {
        var bricks = _context.Bricks.Where(b => request.BrickCodes.Contains(b.Code)).ToList();
        var customer = await _context.Customers.FirstAsync(c => c.Id == request.CustomerId);

        var entity = new Creation
        {
            Created = DateTime.UtcNow,
            Bricks = bricks,
            Customer = customer,
            Description = request.Description,
            ImagePath = request.ImagePath,
            ThumbnailPath = request.ThumbnailPath,
            Title = request.Title
        };

        entity.AddDomainEvent(new CreationCreatedEvent(entity));

        _context.Creations.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
