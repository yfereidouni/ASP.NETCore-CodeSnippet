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

        [HttpGet]
        public List<Movie> Get()
        {
            return moviesAPIContext.Movies.ToList();
        }

        //[HttpGet]
        //public List<Movie> GetById(int id)
        //{
        //    return moviesAPIContext.Movies.Where(x => x.Id == id);
        //}

        //[HttpGet]
        //public Movie GetById(int id)
        //{
        //    var movie = moviesAPIContext.Movies.Find(id);
        //    return movie;
        //}
    }
}
