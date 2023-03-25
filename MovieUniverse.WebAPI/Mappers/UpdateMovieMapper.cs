using MovieUniverse.Data.Models;
using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Mappers
{
    public class UpdateMovieMapper : IUpdateMovieMapper
    {
        public Movie MapToEntity(UpdateMovieDTO dto)
        {
            return new Movie
            {
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate,
                ShortDescription = dto.ShortDescription,
                Genres = dto.Genres.Select(GenreExtensions.MapToEntity).ToList()
            };
        }
    }
}
