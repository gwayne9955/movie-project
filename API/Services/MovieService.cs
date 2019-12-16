using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using movie_project.API.Domain.Queries;
using movie_project.API.Domain.Repositories;
using movie_project.API.Domain.Services;
using movie_project.API.Domain.Services.Communication;
using movie_project.API.Infrastructure;
using movie_project.Models;

namespace movie_project.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieListRepository _movieListRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public MovieService(IMovieRepository movieRepository, IMovieListRepository movieListRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _movieRepository = movieRepository;
            _movieListRepository = movieListRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        //public async Task<QueryResult<Movie>> ListAsync(MoviesQuery query)
        //{
        //    // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
        //    // items per page. I have to compose a cache to avoid returning wrong data.
        //    string cacheKey = GetCacheKeyForMovieListsQuery(query);

        //    //var movieLists = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
        //    //    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        //    //    return _movieListRepository.ListAsync(query);
        //    //});

        //    var movieLists = await _movieRepository.ListAsync(query);

        //    return movieLists;
        //}

        //public async Task<MovieResponse> ListAsync(int id, MoviesQuery query)
        //{
        //    // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
        //    // items per page. I have to compose a cache to avoid returning wrong data.
        //    //string cacheKey = GetCacheKeyForMovieListsQuery(query);

        //    //var movieList = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
        //    //    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        //    //    return _movieListRepository.FindByIdAsync(id);
        //    //});


        //    //return movieList;

        //    var existingMovieList = await _movieRepository.FindByIdAsync(id);

        //    if (existingMovieList == null || !(existingMovieList.ApplicationUserRefId.Equals(query.ApplicationUserRefId)))
        //        return new MovieResponse("Movie not found.");

        //    return new MovieListResponse(existingMovieList);
        //}

        public async Task<MovieResponse> SaveAsync(Movie movie, MoviesQuery query)
        {
            var existingMovieList = await _movieListRepository.FindByIdAsync(movie.MovieListRefId);

            if (existingMovieList == null || !(existingMovieList.ApplicationUserRefId.Equals(query.ApplicationUserRefId)))
                return new MovieResponse("Could not save movie.");

            try
            {
                await _movieRepository.AddAsync(movie);
                await _unitOfWork.CompleteAsync();

                return new MovieResponse(movie);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new MovieResponse($"An error occurred when saving the movie: {ex.Message}");
            }
        }

        //public async Task<MovieListResponse> UpdateAsync(int id, MovieList movieList, MoviesQuery query)
        //{
        //    var existingMovieList = await _movieRepository.FindByIdAsync(id);

        //    if (existingMovieList == null || !(existingMovieList.ApplicationUserRefId.Equals(query.ApplicationUserRefId)))
        //        return new MovieListResponse("MovieList not found.");

        //    existingMovieList.Name = movieList.Name;

        //    try
        //    {
        //        _movieRepository.Update(existingMovieList);
        //        await _unitOfWork.CompleteAsync();

        //        return new MovieListResponse(existingMovieList);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do some logging stuff
        //        return new MovieListResponse($"An error occurred when updating the movie list: {ex.Message}");
        //    }
        //}

        public async Task<MovieResponse> DeleteAsync(int id, MoviesQuery query, string imdbID)
        {
            var existingMovieList = await _movieListRepository.FindByIdAsync(id);
            var existingMovie = await _movieRepository.FindByIdAsync(id, imdbID);

            if (existingMovie == null || existingMovieList == null || !(existingMovieList.ApplicationUserRefId.Equals(query.ApplicationUserRefId)))
                return new MovieResponse("Movie not found.");

            try
            {
                _movieRepository.Remove(existingMovie);
                await _unitOfWork.CompleteAsync();

                return new MovieResponse(existingMovie);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new MovieResponse($"An error occurred when deleting the movie: {ex.Message}");
            }
        }

        //private string GetCacheKeyForMovieListsQuery(MoviesQuery query)
        //{
        //    string key = CacheKeys.MovieListsList.ToString();

        //    if (query.ApplicationUserRefId != null && query.ApplicationUserRefId.Length > 0)
        //    {
        //        key = string.Concat(key, "_", query.ApplicationUserRefId);
        //    }

        //    key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
        //    return key;
        //}
    }
}
