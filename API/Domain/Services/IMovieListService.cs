using System;
using movie_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie_project.API.Domain.Services.Communication;

namespace movie_project.API.Domain.Services
{
    public interface IMovieListService
    {
        Task<IEnumerable<MovieList>> ListAsync();
        Task<SaveMovieListResponse> SaveAsync(MovieList movieList);
    }
}
