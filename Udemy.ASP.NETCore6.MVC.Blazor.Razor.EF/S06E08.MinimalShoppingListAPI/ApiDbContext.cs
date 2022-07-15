using Microsoft.EntityFrameworkCore;

namespace S06E08.MinimalShoppingListAPI;

public class ApiDbContext : DbContext
{
    public DbSet<Grocery> Groceries => Set<Grocery>();
    //OR:
    //public DbSet<Grocery> Groceries { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }
}
