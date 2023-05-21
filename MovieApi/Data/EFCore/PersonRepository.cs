using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data.EFCore
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MovieAppDbContext _context;

        public PersonRepository(MovieAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetByName(string personName)
        {
            return await _context.Person
                .Where(p => p.Name == personName)
                .ToListAsync();
        }
        public async Task Add(Person person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Person person)
        {
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
        }
        public async Task<Person> GetById(int id)
        {
            return await _context.Set<Person>()
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Set<Person>()
            .ToListAsync();
        }
        public async Task<Person> GetByNameAsync(string name)
        {
            return await _context.Set<Person>().FirstOrDefaultAsync(p => p.Name == name);
        }
    }

}
