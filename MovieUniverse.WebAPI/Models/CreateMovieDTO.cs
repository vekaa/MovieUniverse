namespace MovieUniverse.WebAPI.Models
{
    public class CreateMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; } = string.Empty;

        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
