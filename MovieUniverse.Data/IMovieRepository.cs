using MovieUniverse.Data.Models;

namespace MovieUniverse.Data
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
    }
}