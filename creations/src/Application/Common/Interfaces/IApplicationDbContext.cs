using Creations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Creations.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }

    DbSet<Creation> Creations { get; }

    DbSet<Review> Reviews { get; }

    DbSet<Brick> Bricks{ get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
