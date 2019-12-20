using System;
using System.Threading.Tasks;
using movie_project.API.Domain.Queries;
using movie_project.API.Domain.Services.Communication;
using movie_project.Models;

namespace movie_project.API.Domain.Services
{
    public interface IMovieService
    {
        Task<MovieResponse> SaveAsync(Movie movie, MoviesQuery query);
        Task<MovieResponse> UpdateAsync(int id, Movie movie, MoviesQuery query, string imdbID);
        Task<MovieResponse> DeleteAsync(int id, MoviesQuery query, string imdbID);
    }
}
