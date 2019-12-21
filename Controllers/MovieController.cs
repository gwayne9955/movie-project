using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movie_project.API.Resources;
using movie_project.API.Extensions;
using movie_project.API.Domain.Services;
using movie_project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using movie_project.API.Domain.Queries;

namespace movie_project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieController(IMovieService movieService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _movieService = movieService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveMovieResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MoviesQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var moviesQuery = _mapper.Map<MoviesQueryResource, MoviesQuery>(query);

            var movie = _mapper.Map<SaveMovieResource, Movie>(resource);
            var result = await _movieService.SaveAsync(movie, moviesQuery);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieResource = _mapper.Map<Movie, MovieResource>(result.Movie);
            return Ok(movieResource);
        }

        // PUT api/values/5
        [HttpPut("{listId}/{imdbID}")] // the list id, the resource has the imdb id
        public async Task<IActionResult> PostAsync(int listId, string imdbID, [FromBody]EditMovieResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MoviesQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var moviesQuery = _mapper.Map<MoviesQueryResource, MoviesQuery>(query);

            var movie = _mapper.Map<EditMovieResource, Movie>(resource);
            var result = await _movieService.UpdateAsync(listId, movie, moviesQuery, imdbID);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<Movie, MovieResource>(result.Movie);
            return Ok(movieListResource);
        }

        // DELETE api/values/5
        [HttpDelete("{listId}/{imdbID}")] // the list id, the resource has the imdb id
        public async Task<IActionResult> DeleteAsync(int listId, string imdbID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MoviesQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var moviesQuery = _mapper.Map<MoviesQueryResource, MoviesQuery>(query);

            var result = await _movieService.DeleteAsync(listId, moviesQuery, imdbID);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<Movie, MovieResource>(result.Movie);
            return Ok(movieListResource);
        }
    }
}
