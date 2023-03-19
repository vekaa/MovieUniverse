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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all method started"); // Za sve metode..
            var movies = await movieRepository.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}", Name = nameof(GetMovieById))]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public async Task<IActionResult> GetMovieById(int id)
        {
            _logger.LogInformation($"Get movie with id {id}"); // Za sve metode..

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
        [ProducesResponseType(201, Type = typeof(Movie))]
        public async Task<IActionResult> Post(Movie movie)
        {
            await movieRepository.Create(movie);

            //Used when client needs to be rediredted to another action, display the new resource 
            //return CreatedAtAction(nameof(GetById),new {id = movie.Id} , movie);  

            return CreatedAtRoute(
                routeName: nameof(GetMovieById),
                routeValues: new { movie.Id },
                value: movie); //Used when client needs to know how to access

        }


        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, Movie movie) //movie
        {
            var res = await movieRepository.Update(id, movie);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await movieRepository.Delete(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }


    }
}