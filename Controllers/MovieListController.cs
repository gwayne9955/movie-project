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
    public class MovieListController : Controller
    {
        private readonly IMovieListService _movieListService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieListController(IMovieListService movieListService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _movieListService = movieListService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/values
        [HttpGet]
        public async Task<QueryResultResource<MovieListResource>> GetAllAsync([FromQuery] MovieListsQueryResource query)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

            var queryResult = await _movieListService.ListAsync(movieListsQuery);
            var resources = _mapper.Map<QueryResult<MovieList>, QueryResultResource<MovieListResource>>(queryResult);

            return resources;
        }

        // GET api/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MovieListsQueryResource(); // could potentially make these 4 lines a function
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

            var result = await _movieListService.ListAsync(id, movieListsQuery);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
            return Ok(movieListResource);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveMovieListResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MovieListsQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

            var movieList = _mapper.Map<SaveMovieListResource, MovieList>(resource);
            movieList.ApplicationUserRefId = userId;
            var result = await _movieListService.SaveAsync(movieList, movieListsQuery);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
            return Ok(movieListResource);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMovieListResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MovieListsQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

            var movieList = _mapper.Map<SaveMovieListResource, MovieList>(resource);
            var result = await _movieListService.UpdateAsync(id, movieList, movieListsQuery);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
            return Ok(movieListResource);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MovieListsQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

            var result = await _movieListService.DeleteAsync(id, movieListsQuery);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
            return Ok(movieListResource);
        }
    }
}
