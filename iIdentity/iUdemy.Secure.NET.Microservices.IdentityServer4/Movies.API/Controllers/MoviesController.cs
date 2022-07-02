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
    }
}
