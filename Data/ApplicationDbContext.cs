using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VintageGameStore.Models; // This allows to the game and category models

namespace VintageGameStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // To create the tables in the db
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
