using MovieUniverse.Data.Models;
using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Mappers
{
    public class CreateMovieMapper : ICreateMovieMapper
    {
        public CreateMovieDTO MapToDTO(Movie entity)
        {
            return new CreateMovieDTO
            {
                Title = entity.Title,
                ReleaseDate = entity.ReleaseDate,
                ShortDescription = entity.ShortDescription,
                Genres = entity.Genres.Select(GenreExtensions.MapToDTO).ToList()
            };
        }

        public Movie MapToEntity(CreateMovieDTO dto)
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
