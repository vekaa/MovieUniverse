using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Mappers
{
    public class GetMovieMapper: IGetMovieMapper
    {

        public GetMovieDTO MapToDTO(Data.Models.Movie movie)
        {
            return new GetMovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                ShortDescription = movie.ShortDescription,
                Genres = movie.Genres.Select(MapGenre).ToList()
            };
        }

    }
}
