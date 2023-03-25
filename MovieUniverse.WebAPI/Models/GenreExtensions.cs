
namespace MovieUniverse.WebAPI.Models
{
    public static class GenreExtensions
    {
        public static Genre MapToDTO(this Data.Models.Genre genre)
        {
            return (Genre)Enum.Parse(typeof(Genre), genre.ToString());
        }

        public static Data.Models.Genre MapToEntity(this Genre genre)
        {
            return (Data.Models.Genre)Enum.Parse(typeof(Data.Models.Genre), genre.ToString());
        }
    }
    
}
