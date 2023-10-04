using Microsoft.EntityFrameworkCore;
using Recipator.Models;

namespace Recipator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        

            // Configure the Id column as an auto-increment column
            modelBuilder.Entity<Recipe>()
            .Property(r => r.Id)
            .UseIdentityColumn();
        }
    }
}
