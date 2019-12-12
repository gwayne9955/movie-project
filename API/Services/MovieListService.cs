using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie_project.Models;
using movie_project.API.Domain.Services;

namespace movie_project.API.Services
{
    public class MovieListService : IMovieListService
    {
        public async Task<IEnumerable<MovieList>> ListAsync()
        {
        }
    }
}
