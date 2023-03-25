using Microsoft.AspNetCore.Mvc;
using MovieUniverse.Data;
using MovieUniverse.Data.Models;
using MovieUniverse.WebAPI.Mappers;
using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {

        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieRepository movieRepository;
        private readonly IGetMovieMapper getMovieMapper;
        private readonly ICreateMovieMapper createMovieMapper;
        private readonly IUpdateMovieMapper updateMovieMapper;


        public MoviesController(ILogger<MoviesController> logger, IMovieRepository movieRepository, IGetMovieMapper getMovieMapper, ICreateMovieMapper createMovieMapper, IUpdateMovieMapper updateMovieMapper)
        {
            _logger = logger;
            this.movieRepository = movieRepository;
            this.getMovieMapper = getMovieMapper;
            this.createMovieMapper = createMovieMapper;
            this.updateMovieMapper = updateMovieMapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetMovieDTO>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all method started");
            var movies = await movieRepository.GetAll();
            return Ok(movies.Select(getMovieMapper.MapToDTO));
        }

        [HttpGet("{id}", Name = nameof(GetMovieById))]
        [ProducesResponseType(200, Type = typeof(GetMovieDTO))]
        public async Task<IActionResult> GetMovieById(int id)
        {
            _logger.LogInformation($"Get movie with id {id}");

            var movie = await movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                //return Ok(movie);
                return Ok(getMovieMapper.MapToDTO(movie));
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateMovieDTO))]
        public async Task<IActionResult> Post(CreateMovieDTO movieDTO)
        {
            var movie = this.createMovieMapper.MapToEntity(movieDTO);
            await movieRepository.Create(movie); 

            return CreatedAtRoute(
                routeName: nameof(GetMovieById),
                routeValues: new { movie.Id },
                value: this.createMovieMapper.MapToDTO(movie)); //Used when client needs to know how to access
        }


        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, UpdateMovieDTO movieDTO) //movie
        {
            var movie = this.updateMovieMapper.MapToEntity(movieDTO);
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