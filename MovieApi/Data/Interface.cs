using MovieApi.Models;

namespace MovieApi.Data
{
    public interface IGenreRepository
    {
        Task<IEnumerable<string>> GetGenresByPersonName(string personName);
        Task<Genre> GetByName(string name);
        Task Add(Genre genre);

    }
}
