using System;
using movie_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie_project.API.Domain.Services.Communication;
using movie_project.API.Domain.Queries;

namespace movie_project.API.Domain.Services
{
    public interface IMovieListService
    {
        Task<QueryResult<MovieList>> ListAsync(MovieListsQuery query);
        Task<MovieListResponse> ListAsync(int id, MovieListsQuery query);
        Task<MovieListResponse> SaveAsync(MovieList movieList, MovieListsQuery query);
        Task<MovieListResponse> UpdateAsync(int id, MovieList movieList, MovieListsQuery query);
        Task<MovieListResponse> DeleteAsync(int id, MovieListsQuery query);
    }
}
