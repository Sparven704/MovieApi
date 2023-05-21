using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet("{personName}")]
        public async Task<ActionResult<IEnumerable<string>>> GetGenresByPersonName(string personName)
        {
            var genreNames = await _genreRepository.GetGenresByPersonName(personName);
            return Ok(genreNames);
        }
    }
}
