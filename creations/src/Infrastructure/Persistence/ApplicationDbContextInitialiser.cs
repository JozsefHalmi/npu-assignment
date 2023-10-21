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

        //if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //{
        //    await _roleManager.CreateAsync(administratorRole);
        //}

        //// Default users
        //var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        //if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        //{
        //    await _userManager.CreateAsync(administrator, "Administrator1!");
        //    if (!string.IsNullOrWhiteSpace(administratorRole.Name))
        //    {
        //        await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        //    }
        //}

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
                
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Bricks.Any())
        {
            _context.Bricks.Add(new Brick()
            {
                Code = "A01",
                Name = "Small brick"
            });

            _context.Bricks.Add(new Brick()
            {
                Code = "B01",
                Name = "Medium brick"
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Creations.Any())
        {
            _context.Creations.Add(new Creation()
            {
                Customer = _context.Customers.First(),
                Bricks = new List<Brick>() { _context.Bricks.First(b => b.Code == "B01"), _context.Bricks.First(b => b.Code == "A01") },
                Description = "ABCD",
                ThumbnailPath = "abcd-thumbnail.png",
                ImagePath = "abcd-image.png",
                Name = "SomeName - ABCD"
            });

            _context.Creations.Add(new Creation()
            {
                Customer = _context.Customers.First(),
                Bricks = new List<Brick>() { _context.Bricks.First(b => b.Code == "B01") },
                Description = "EFGH",
                ThumbnailPath = "efgh-thumbnail.png",
                ImagePath = "efgh-image.png",
                Name = "SomeName - EFGH"
            });
            await _context.SaveChangesAsync();
        }
    }
}
