using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public class ShoppingListContext : DbContext
{
    public DbSet<Grocery> Groceries { get; set; }
    public ShoppingListContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
