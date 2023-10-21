using Creations.Application.Common.Interfaces;
using Creations.Application.Creations.Queries.GetCreations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.TodoLists.Commands.CreateTodoList;
public class GetCreationsQueryValidator : AbstractValidator<GetCreationsQuery>
{
    private readonly IApplicationDbContext _context;

    public GetCreationsQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.BrickCode)
            .NotEmpty().WithMessage("Brick code is required.")
            //.MustAsync(Exist).WithMessage("The specified title already exists.")
            ;
    }

    public async Task<bool> Exist(string title, CancellationToken cancellationToken)
    {
        return await _context.Bricks
            .AllAsync(l => l.Code != title, cancellationToken);
    }
}
