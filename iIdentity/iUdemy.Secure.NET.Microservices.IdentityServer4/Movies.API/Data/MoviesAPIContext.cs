using Microsoft.EntityFrameworkCore;
using Movies.API.Model;

namespace Movies.API.Data;

public class MoviesAPIContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public MoviesAPIContext(DbContextOptions options) : base(options)
    {
    }
}
