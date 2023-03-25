using MovieUniverse.Data.Models;
using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Mappers
{
    public interface ICreateMovieMapper
    {
        Movie MapToEntity(CreateMovieDTO dto);
        CreateMovieDTO MapToDTO(Movie entity);
    }
}
