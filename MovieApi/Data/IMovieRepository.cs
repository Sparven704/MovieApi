using MovieApi.Models;

namespace MovieApi.Data
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetByPersonName(string personName);
        Task Add(Movie movie);
        Task Update(Movie movie);
        Task Delete(Movie movie);
        Task<Movie> GetById(int id);
        Task<IEnumerable<Movie>> GetAll();
        Task UpdateRating(int movieId, int personId, int rating);
        Task<Movie> AddMovieAsync(Movie movie);
    }
}
