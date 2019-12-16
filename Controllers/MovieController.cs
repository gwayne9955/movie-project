using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movie_project.API.Resources;
using movie_project.API.Extensions;
using movie_project.API.Domain.Services;
using movie_project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using movie_project.API.Domain.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/values
        //[HttpGet]
        //public async Task<QueryResultResource<MovieListResource>> GetAllAsync([FromQuery] MovieListsQueryResource query)
        //{
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    query.ApplicationUserRefId = userId;
        //    var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

        //    var queryResult = await _movieListService.ListAsync(movieListsQuery);
        //    var resources = _mapper.Map<QueryResult<MovieList>, QueryResultResource<MovieListResource>>(queryResult);

        //    return resources;
        //}

        // GET api/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var query = new MovieListsQueryResource(); // could potentially make these 4 lines a function
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    query.ApplicationUserRefId = userId;
        //    var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

        //    var result = await _movieListService.ListAsync(id, movieListsQuery);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
        //    return Ok(movieListResource);
        //}

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
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMovieListResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var query = new MovieListsQueryResource();
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    query.ApplicationUserRefId = userId;
        //    var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

        //    var movieList = _mapper.Map<SaveMovieListResource, MovieList>(resource);
        //    var result = await _movieListService.UpdateAsync(id, movieList, movieListsQuery);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
        //    return Ok(movieListResource);
        //}

        // DELETE api/values/5
        [HttpDelete("{id}")] // the list id, the resource has the imdb id
        public async Task<IActionResult> DeleteAsync(int id, [FromBody]SaveMovieResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var query = new MoviesQueryResource();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.ApplicationUserRefId = userId;
            var moviesQuery = _mapper.Map<MoviesQueryResource, MoviesQuery>(query);

            var result = await _movieService.DeleteAsync(id, moviesQuery, resource.imdbID);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<Movie, MovieResource>(result.Movie);
            return Ok(movieListResource);
        }
    }
}
