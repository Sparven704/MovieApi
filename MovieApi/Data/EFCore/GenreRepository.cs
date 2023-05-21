using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data.EFCore
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieAppDbContext _context;

        public GenreRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetGenresByPersonName(string personName)
        {
            var genreNames = await _context.Set<Genre>()
                .Where(g => g.PersonGenre.Any(pg => pg.Person.Name == personName))
                .Select(g => g.Name)
                .ToListAsync();

            return genreNames;
        }
        public async Task<Genre> GetByName(string name)
        {
            return await _context.Set<Genre>().FirstOrDefaultAsync(g => g.Name == name);
        }
        public async Task Add(Genre genre)
        {
            await _context.Set<Genre>().AddAsync(genre);
            await _context.SaveChangesAsync();
        }

    }

}
