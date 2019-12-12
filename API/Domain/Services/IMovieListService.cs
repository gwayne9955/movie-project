using System;
using movie_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace movie_project.API.Domain.Services
{
    public interface IMovieListService
    {
        Task<IEnumerable<MovieList>> ListAsync();
    }
}
