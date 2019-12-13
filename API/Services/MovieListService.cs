using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie_project.Models;
using movie_project.API.Domain.Services;
using movie_project.API.Domain.Repositories;
using movie_project.API.Resources;
using movie_project.API.Domain.Services.Communication;
using movie_project.API.Domain.Queries;
using movie_project.API.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace movie_project.API.Services
{
    public class MovieListService : IMovieListService
    {
        private readonly IMovieListRepository _movieListRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public MovieListService(IMovieListRepository movieListRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _movieListRepository = movieListRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<QueryResult<MovieList>> ListAsync(MovieListsQuery query)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            string cacheKey = GetCacheKeyForMovieListsQuery(query);

            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _movieListRepository.ListAsync(query);
            });

            return products;
        }

        public async Task<MovieListResponse> SaveAsync(MovieList movieList)
        {
            try
            {
                await _movieListRepository.AddAsync(movieList);
                await _unitOfWork.CompleteAsync();

                return new MovieListResponse(movieList);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new MovieListResponse($"An error occurred when saving the move list: {ex.Message}");
            }
        }

        public async Task<MovieListResponse> UpdateAsync(int id, MovieList movieList)
        {
            var existingMovieList = await _movieListRepository.FindByIdAsync(id);

            if (existingMovieList == null)
                return new MovieListResponse("MovieList not found.");

            existingMovieList.Name = movieList.Name;

            try
            {
                _movieListRepository.Update(existingMovieList);
                await _unitOfWork.CompleteAsync();

                return new MovieListResponse(existingMovieList);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new MovieListResponse($"An error occurred when updating the movie list: {ex.Message}");
            }
        }

        public async Task<MovieListResponse> DeleteAsync(int id)
        {
            var existingMovieList = await _movieListRepository.FindByIdAsync(id);

            if (existingMovieList == null)
                return new MovieListResponse("MovieList not found.");

            try
            {
                _movieListRepository.Remove(existingMovieList);
                await _unitOfWork.CompleteAsync();

                return new MovieListResponse(existingMovieList);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new MovieListResponse($"An error occurred when deleting the movie list: {ex.Message}");
            }
        }

        private string GetCacheKeyForMovieListsQuery(MovieListsQuery query)
        {
            string key = CacheKeys.MovieListsList.ToString();

            if (query.ApplicationUserRefId != null && query.ApplicationUserRefId.Length > 0)
            {
                key = string.Concat(key, "_", query.ApplicationUserRefId);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }
    }
}
