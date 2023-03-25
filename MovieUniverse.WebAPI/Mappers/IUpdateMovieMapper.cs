using MovieUniverse.Data.Models;
using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Mappers
{
    public interface IUpdateMovieMapper
    {
        Movie MapToEntity(UpdateMovieDTO dto);
    }
}
