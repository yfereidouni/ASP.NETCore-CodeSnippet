using Microsoft.EntityFrameworkCore;

namespace S06E03.ShoppingListAPI.Models;

public class ShoppingListContext : DbContext
{
    public DbSet<Grocery> Groceries { get; set; }

    public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
        : base(options)
    {
    }
}
