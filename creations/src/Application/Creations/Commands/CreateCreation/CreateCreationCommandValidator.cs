using Creations.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.Creations.Commands.CreateCreation;
public class CreateCreationCommandValidator : AbstractValidator<CreateCreationCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCreationCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty()
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");

        RuleFor(v => v.Description)
            .NotEmpty();

        RuleFor(v => v.ThumbnailPath)
            .NotEmpty();

        RuleFor(v => v.ImagePath)
            .NotEmpty();

        RuleFor(v => v.CustomerId)
            .MustAsync(CustomerExists).WithMessage("The specified customer does not exist.");

        RuleFor(v => v.BrickCodes)
           .NotNull()
           .NotEmpty()
           .Must(BrickCodesExist).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Creations
            .AllAsync(l => l.Title != title, cancellationToken);
    }

    public async Task<bool> CustomerExists(int customerId, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AnyAsync(l => l.Id == customerId, cancellationToken);
    }

    public bool BrickCodesExist(IEnumerable<string> brickCodes)
    {
        return brickCodes?.All(bc => _context.Bricks.Any(b => b.Code == bc)) ?? false;
    }
}
