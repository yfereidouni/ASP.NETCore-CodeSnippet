using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movies.API.Model;

namespace Movies.API.Data;

public class SeedData
{
    #region Way-1-of-Seed-Data
    public static void Initialize(MoviesAPIContext context)
    {
        if (!context.Movies.Any())
        {
            context.Movies.AddRange(
                new Movie
                {
                    Id = 1,
                    Title = "Movie-01",
                    Genre = "Action",
                    Rating = "5",
                    ReleaseDate = DateTime.Now,
                    ImageUrl = "image/src",
                    Owner = "Yasser"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Movie-02",
                    Genre = "Action",
                    Rating = "4",
                    ReleaseDate = DateTime.Now.AddDays(-300),
                    ImageUrl = "image/src",
                    Owner = "Khalil"
                },
                new Movie
                {
                    Id = 3,
                    Title = "Movie-03",
                    Genre = "SienceFiction",
                    Rating = "5",
                    ReleaseDate = DateTime.Now.AddDays(-500),
                    ImageUrl = "image/src",
                    Owner = "Karim"
                },
                new Movie
                {
                    Id = 4,
                    Title = "Movie-04",
                    Genre = "Romance",
                    Rating = "3",
                    ReleaseDate = DateTime.Now.AddDays(-150),
                    ImageUrl = "image/src",
                    Owner = "Meysam"
                }
            );
            context.SaveChanges();
        }
    }
    #endregion

    #region Way-2-of-Seed-Data
    //public static void Initialize(IServiceProvider serviceProvider)
    //{
    //    using (var context = new MoviesAPIContext(serviceProvider
    //        .GetRequiredService<DbContextOptions<MoviesAPIContext>>()))
    //    {
    //        if (!context.Movies.Any())
    //        {
    //            context.Movies.AddRange(
    //                new Movie
    //                {
    //                    Id = 1,
    //                    Title = "Movie-01",
    //                    Genre = "Action",
    //                    Rating = "5",
    //                    ReleaseDate = DateTime.Now,
    //                    ImageUrl = "image/src",
    //                    Owner = "Yasser"
    //                },
    //                new Movie
    //                {
    //                    Id = 2,
    //                    Title = "Movie-02",
    //                    Genre = "Action",
    //                    Rating = "4",
    //                    ReleaseDate = DateTime.Now.AddDays(-300),
    //                    ImageUrl = "image/src",
    //                    Owner = "Khalil"
    //                },
    //                new Movie
    //                {
    //                    Id = 3,
    //                    Title = "Movie-03",
    //                    Genre = "SienceFiction",
    //                    Rating = "5",
    //                    ReleaseDate = DateTime.Now.AddDays(-500),
    //                    ImageUrl = "image/src",
    //                    Owner = "Karim"
    //                },
    //                new Movie
    //                {
    //                    Id = 4,
    //                    Title = "Movie-04",
    //                    Genre = "Romance",
    //                    Rating = "3",
    //                    ReleaseDate = DateTime.Now.AddDays(-150),
    //                    ImageUrl = "image/src",
    //                    Owner = "Meysam"
    //                }
    //            );
    //            context.SaveChanges();
    //        }
    //    }
    //}
    #endregion
}
