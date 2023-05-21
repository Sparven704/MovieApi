using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data.EFCore
{
    public class TMDBRepository : ITMDBRepository
    {
        private readonly MovieAppDbContext _context;

        public TMDBRepository(MovieAppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Genre>> GetMoviesByGenre(int genreId)
        {
            return await _context.Genre.Where(m => m.Id == genreId).ToListAsync();
        }
    }

}
