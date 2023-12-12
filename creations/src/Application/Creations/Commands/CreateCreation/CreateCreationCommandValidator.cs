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
            .MustAsync(CustomerExists).WithMessage("The specified customer does not exist.")
            .MustAsync(PrivacyPolicyAccepted).WithMessage("The specified customer hasn't accepted the privacy policy.");

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

    public async Task<bool> PrivacyPolicyAccepted(int customerId, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstAsync(l => l.Id == customerId, cancellationToken);

        return customer.PrivacyPolicyAccepted;
    }

    public bool BrickCodesExist(IEnumerable<string> brickCodes)
    {
        return brickCodes?.All(bc => _context.Bricks.Any(b => b.Code == bc)) ?? false;
    }
}
