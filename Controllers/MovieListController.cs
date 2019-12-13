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

            //var query = new MovieListsQueryResource(userId); // create a MovieListsQueryResource Object with user id

            var movieListsQuery = _mapper.Map<MovieListsQueryResource, MovieListsQuery>(query);

            var queryResult = await _movieListService.ListAsync(movieListsQuery);
            var resources = _mapper.Map<QueryResult<MovieList>, QueryResultResource<MovieListResource>>(queryResult);

            return resources;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveMovieListResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var movieList = _mapper.Map<SaveMovieListResource, MovieList>(resource);
            var result = await _movieListService.SaveAsync(movieList);

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

            var movieList = _mapper.Map<SaveMovieListResource, MovieList>(resource);
            var result = await _movieListService.UpdateAsync(id, movieList);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
            return Ok(movieListResource);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _movieListService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var movieListResource = _mapper.Map<MovieList, MovieListResource>(result.MovieList);
            return Ok(movieListResource);
        }
    }
}
