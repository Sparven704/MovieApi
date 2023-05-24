using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        //[HttpGet("{personName}")]
        //public async Task<ActionResult<IEnumerable<(string Name, string Link)>>> GetMoviesByPersonName(string personName)
        //{
        //    var movieNamesAndLinks = await _movieRepository.GetMoviesByPersonName(personName);
        //    return Ok(movieNamesAndLinks);
        //}
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPersonRepository _personRepository;

        public MovieController(IMovieRepository movieRepository, IGenreRepository genreRepository, IPersonRepository personRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _personRepository = personRepository;
        }

        [HttpGet("get/{personName}")]
        public async Task<ActionResult<IEnumerable<GetMovies>>> GetMoviesByPersonName(string personName)
        {
            var person = await _personRepository.GetByName(personName);

            if (person == null)
            {
                return NotFound();
            }

            var movies = await _movieRepository.GetByPersonName(personName);

            var movieDtos = movies.Select(m => new GetMovies
            {
                Name = m.Name,
                Link = m.Link,
                Rating = m.Rating
            });

            return Ok(movieDtos);
        }

        [HttpPut("put/{movieId}/rating/{personId}/{newRating}")]
        public async Task<IActionResult> UpdateRating(int movieId, int personId, int newRating)
        {
            await _movieRepository.UpdateRating(movieId, personId, newRating);

            return Ok();
        }
        [HttpPost("post")]
        public async Task<IActionResult> CreateMovie(MovieDto request)
        {
            var genre = await _genreRepository.GetByName(request.GenreName);
            if (genre == null)
            {
                return BadRequest("Invalid genre name");
            }

            var person = await _personRepository.GetByNameAsync(request.PersonName);
            if (person == null)
            {
                return BadRequest("Invalid person name");
            }

            var movie = new Movie
            {
                Name = request.Name,
                Link = request.Link,
                PersonId = person.Id,
                GenreId = genre.Id
            };

            await _movieRepository.AddMovieAsync(movie);

            return Ok(movie);
        }
    }
}
