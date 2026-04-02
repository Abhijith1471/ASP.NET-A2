using Microsoft.EntityFrameworkCore;
using VintageGameStore.Data;

namespace VintageGameStore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Categories.Any()) return;

                var nintendo = new Category { Name = "Nintendo", Description = "Classic Nintendo consoles" };
                var playstation = new Category { Name = "PlayStation", Description = "Sony consoles" };

                context.Categories.AddRange(nintendo, playstation);
                context.SaveChanges();

                context.Games.AddRange(
                    new Game { Title = "Super Mario Bros 3", Price = 55.00m, ReleaseYear = 1988, CategoryId = nintendo.Id },
                    new Game { Title = "Final Fantasy VII", Price = 70.00m, ReleaseYear = 1997, CategoryId = playstation.Id }
                );
                context.SaveChanges();
            }
        }
    }
}