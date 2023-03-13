using MovieUniverse.Data.Models;

namespace MovieUniverse.Data
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetMovieById(int id);
        Task<Movie> Create(Movie movie);
        Task<Movie> Update(int id, Movie movie);
        Task<bool> Delete(int id); 
    }
}