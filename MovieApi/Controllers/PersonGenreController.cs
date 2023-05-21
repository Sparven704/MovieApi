using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonGenreController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPersonGenreRepository _personGenreRepository;

        public PersonGenreController(IPersonRepository personRepository, IGenreRepository genreRepository, IPersonGenreRepository personGenreRepository)
        {
            _personRepository = personRepository;
            _genreRepository = genreRepository;
            _personGenreRepository = personGenreRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonGenreAsync(CreatePersonGenre createPersonGenre)
        {
            // Get person by name
            var person = await _personRepository.GetByNameAsync(createPersonGenre.PersonName);

            if (person == null)
            {
                // Throws exception if no person is found
                throw new Exception("Error");
            }

            // Get genre by name
            var genre = await _genreRepository.GetByName(createPersonGenre.GenreName);

            if (genre == null)
            {
                // Throws exception if no Genre is found
                throw new Exception("Error");
            }

            // Create new person genre
            var personGenre = new PersonGenre { Person = person, Genre = genre };
            await _personGenreRepository.Add(personGenre);

            return Ok(new { Id = personGenre.Id });
        }


    }
}
