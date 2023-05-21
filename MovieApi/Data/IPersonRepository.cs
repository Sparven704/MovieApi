using MovieApi.Models;

namespace MovieApi.Data
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetById(int id);
        Task<IEnumerable<Person>> GetByName(string personName);
        Task Add(Person person);
        Task Update(Person person);
        Task Delete(Person person);
        Task<Person> GetByNameAsync(string name);
    }
}
