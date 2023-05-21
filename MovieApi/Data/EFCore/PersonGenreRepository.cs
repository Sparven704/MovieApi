using MovieApi.Models;

namespace MovieApi.Data.EFCore
{
    public class PersonGenreRepository : IPersonGenreRepository
    {
        private readonly MovieAppDbContext _context;

        public PersonGenreRepository(MovieAppDbContext context)
        {
            _context = context;
        }
        public async Task Add(PersonGenre personGenre)
        {
            await _context.Set<PersonGenre>().AddAsync(personGenre);
            await _context.SaveChangesAsync();
        }
    }

}
