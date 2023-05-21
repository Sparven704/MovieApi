using MovieApi.Models;

namespace MovieApi.Data
{
    public interface IPersonGenreRepository
    {
        Task Add(PersonGenre personGenre);
    }
}
