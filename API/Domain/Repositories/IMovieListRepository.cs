using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie_project.Models;

namespace movie_project.API.Domain.Repositories
{
    public interface IMovieListRepository
    {
        Task<IEnumerable<MovieList>> ListAsync();
        Task AddAsync(MovieList movieList);
    }
}
