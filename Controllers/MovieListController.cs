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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace movie_project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieListController : Controller
    {
        private readonly IMovieListService _movielistService;

        public MovieListController(IMovieListService movielistService)
        {
            _movielistService = movielistService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<MovieList>> GetAllAsync()
        {
            return await _movielistService.ListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MovieListResource movieList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
