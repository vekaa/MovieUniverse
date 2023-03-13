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

        [HttpGet("{id}", Name = nameof(GetMovieById))]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Post(Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            await movieRepository.Create(movie);

            //Used when client needs to be rediredted to another action, display the new resource 
            //return CreatedAtAction(nameof(GetById),new {id = movie.Id} , movie);  

            return CreatedAtRoute(
                routeName: nameof(GetMovieById),
                routeValues: new { Id = movie.Id },
                value: movie); //Used when client needs to know how to access

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Movie movie)
        {
            var res = await movieRepository.Update(id, movie);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return new NoContentResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await movieRepository.Delete(id);
            if (res == false)
            {
                return NotFound();
            }
            return new NoContentResult();
        }


    }
}