using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data.EFCore
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAppDbContext _context;

        public MovieRepository(MovieAppDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> GetById(int id)
        {
            return await _context.Set<Movie>()
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Set<Movie>()
                .Include(m => m.Person)
                .ToListAsync();
        }

        public async Task UpdateRating(int movieId, int personId, int newRating)
        {
            var movie = await _context.Set<Movie>()
                .FirstOrDefaultAsync(m => m.Id == movieId && m.PersonId == personId);
            if (newRating > 10)
            {
                throw new Exception("The max rating is 10");
            }
            else if (movie != null)
            {
                movie.Rating = newRating;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Movie>> GetByPersonName(string personName)
        {
            return await _context.Movie
                .Where(m => m.Person.Name == personName)
                .ToListAsync();
        }

        public async Task Add(Movie movie)
        {
            await _context.Movie.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Movie movie)
        {
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
        }
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }

}
