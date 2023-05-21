namespace MovieApi.Models
{
    public class TMDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public int GenreId { get; set; }
    }
}
