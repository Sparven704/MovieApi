using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TMDBController : ControllerBase
    {
        private readonly ITMDBRepository _repository;

        public TMDBController(ITMDBRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("genres/{genreName}/movies")]
        public async Task<ActionResult<List<TMDB>>> GetMoviesByGenre(string genreName)
        {
            // Use the genre name to retrieve the corresponding genre ID
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.themoviedb.org/3/genre/movie/list?api_key=0ef213470193edbb5434dfeabe288733&language=en-US");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            int genreId = 0;
            foreach (var genre in result.genres)
            {
                var genreNameValue = (string)genre.name;
                if (genreNameValue.ToLower() == genreName.ToLower())
                {
                    genreId = genre.id;
                    break;
                }
            }

            // If no matching genre found, return 404 Not Found
            if (genreId == 0)
            {
                return NotFound();
            }

            // Call the repository to get the list of movies by genre ID
            var movies = await _repository.GetMoviesByGenre(genreId);

            // Append the genreId as a query parameter to the API endpoint URL
            var apiUrl = $"https://api.themoviedb.org/3/discover/movie?api_key=0ef213470193edbb5434dfeabe288733&language=en-US&sort_by=popularity.asc&include_adult=false&include_video=false&page=1&with_genres={genreId}";

            // Call the external API to get the list of movies by genre ID
            var apiResponse = await httpClient.GetAsync(apiUrl);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<TMDBApiResponse>(apiContent);

            // If the external API call was unsuccessful, return 500 Internal Server Error
            if (apiResponse.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode(500);
            }

            // Map the list of movies from the external API to a list of TMDB models
            var tmdbMovies = apiResult.Results.Select(x => new TMDB
            {
                Id = x.Id,
                Title = x.Title,
                ReleaseDate = x.ReleaseDate,
                Overview = x.Overview,
                PosterPath = x.PosterPath
            }).ToList();

            // Return the list of TMDB models
            return Ok(tmdbMovies);
        }
    }
}
