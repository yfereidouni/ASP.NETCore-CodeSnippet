using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Data;
using Movies.API.Model;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly MoviesAPIContext moviesAPIContext;

        public MoviesController(ILogger<MoviesController> logger, MoviesAPIContext moviesAPIContext)
        {
            _logger = logger;
            this.moviesAPIContext = moviesAPIContext;
        }

        [HttpGet("Get")]
        public List<Movie> Get()
        {
            return moviesAPIContext.Movies.ToList();
        }

        [HttpGet("GetById")]
        public async Task<Movie> GetById([FromQuery] int id)
        {
            return await moviesAPIContext.Movies.FindAsync(id);
        }

        [HttpPost("Create")]
        public async Task<Movie> Create([FromBody] Movie model)
        {
            var movieInDb = await moviesAPIContext.Movies.FindAsync(model.Id);

            if (movieInDb == null)
            {
                Movie movie = new()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Genre = model.Genre,
                    ImageUrl = model.ImageUrl,
                    Owner = model.Owner,
                    Rating = model.Rating,
                    ReleaseDate = model.ReleaseDate
                };
                await moviesAPIContext.AddAsync(movie);
                await moviesAPIContext.SaveChangesAsync();
                return movie;
            }
            else
            {
                /// Create new Movie if ID exist in DB
                Movie movie = new()
                {
                    Title = model.Title,
                    Genre = model.Genre,
                    ImageUrl = model.ImageUrl,
                    Owner = model.Owner,
                    Rating = model.Rating,
                    ReleaseDate = model.ReleaseDate
                };
                await moviesAPIContext.AddAsync(movie);
                await moviesAPIContext.SaveChangesAsync();
                return movie;

                ///// Update Movie if ID exist in DB
                //movieInDb.Id = model.Id;
                //movieInDb.Title = model.Title;
                //movieInDb.Genre = model.Genre;
                //movieInDb.ImageUrl = model.ImageUrl;
                //movieInDb.Owner = model.Owner;
                //movieInDb.Rating = model.Rating;
                //movieInDb.ReleaseDate = model.ReleaseDate;

                //await moviesAPIContext.AddAsync(movieInDb);
                //await moviesAPIContext.SaveChangesAsync();
            }
        }

        [HttpPut("Update")]
        public async Task<Movie> Update([FromBody] Movie model)
        {
            var movieInDb = await moviesAPIContext.Movies.FindAsync(model.Id);

            if (movieInDb != null)
            {
                movieInDb.Id = model.Id;
                movieInDb.Title = model.Title;
                movieInDb.Genre = model.Genre;
                movieInDb.ImageUrl = model.ImageUrl;
                movieInDb.Owner = model.Owner;
                movieInDb.Rating = model.Rating;
                movieInDb.ReleaseDate = model.ReleaseDate;

                //await moviesAPIContext.AddAsync(movieInDb);
                await moviesAPIContext.SaveChangesAsync();
            }

            return movieInDb;
        }

        [HttpDelete("Delete")]
        public async Task<Movie> Delete([FromQuery] int id)
        {
            var movieInDb = await moviesAPIContext.Movies.FindAsync(id);

            if (movieInDb != null)
            {
                moviesAPIContext.Movies.Remove(movieInDb);
                await moviesAPIContext.SaveChangesAsync();
            }

            return movieInDb;
        }
    }
}
