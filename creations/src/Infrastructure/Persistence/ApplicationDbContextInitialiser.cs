using Creations.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Creations.Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, 
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }

        if (!_context.Customers.Any())
        {
            _context.Customers.Add(new Customer()
            {
                Id=999,
                FirstName = "Test",
                LastName = "Test",
                PrivacyPolicyAccepted = true
            });

            _context.Customers.Add(new Customer()
            {
                Id = 1000,
                FirstName = "Test not accepted",
                LastName = "Test not accepted",
                PrivacyPolicyAccepted = false
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Bricks.Any())
        {
            _context.Bricks.Add(new Brick()
            {
                Id = 1,
                Code = "A01",
            });

            _context.Bricks.Add(new Brick()
            {
                Id = 2,
                Code = "B01",
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Creations.Any())
        {
            _context.Creations.Add(new Creation()
            {
                Id = 1001,
                Customer = _context.Customers.First(),
                Bricks = new List<Brick>() { _context.Bricks.First(b => b.Code == "B01"), _context.Bricks.First(b => b.Code == "A01") },
                Description = "ABCD",
                ThumbnailPath = "abcd-thumbnail.png",
                ImagePath = "abcd-image.png",
                Title = "SomeName - ABCD"
            });

            _context.Creations.Add(new Creation()
            {
                Id = 1002,
                Customer = _context.Customers.First(),
                Bricks = new List<Brick>() { _context.Bricks.First(b => b.Code == "B01") },
                Description = "EFGH",
                ThumbnailPath = "efgh-thumbnail.png",
                ImagePath = "efgh-image.png",
                Title = "SomeName - EFGH"
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Reviews.Any())
        {
            _context.Reviews.Add(new Review()
            {
                Id = 1,
                Customer = _context.Customers.First(),
                Creation = _context.Creations.First(),
                CreativityScore = 5,
                UniquenessScore = 9,
                Text = "Some text"
            });
            _context.Reviews.Add(new Review()
            {
                Id = 2,
                Customer = _context.Customers.First(),
                Creation = _context.Creations.First(),
                CreativityScore = 4,
                UniquenessScore = 3,
                Text = "Some text2"
            });
      
            await _context.SaveChangesAsync();
        }
    }
}
