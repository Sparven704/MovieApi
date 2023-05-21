using MovieApi.Models;

namespace MovieApi.Data
{
    public interface ITMDBRepository
    {
        Task<List<Genre>> GetMoviesByGenre(int genreId);
    }
}
