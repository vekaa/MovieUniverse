using Microsoft.AspNetCore.Mvc;
using MovieUniverse.Data;
using MovieUniverse.Data.Models;

namespace MovieUniverse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {

        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieRepository movieRepository;

        public MoviesController(ILogger<MoviesController> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var movies = await movieRepository.GetAll();
            return Ok(movies);
        }

    }
}