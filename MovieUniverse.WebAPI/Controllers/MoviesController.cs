using Microsoft.AspNetCore.Mvc;

namespace MovieUniverse.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }


    }
}