using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie_project.Models;
using movie_project.API.Domain.Services;
using movie_project.API.Domain.Repositories;
using movie_project.API.Resources;
using movie_project.API.Domain.Services.Communication;

namespace movie_project.API.Services
{
    public class MovieListService : IMovieListService
    {
        private readonly IMovieListRepository _movieListRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MovieListService(IMovieListRepository movieListRepository, IUnitOfWork unitOfWork)
        {
            _movieListRepository = movieListRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MovieList>> ListAsync()
        {
            return await _movieListRepository.ListAsync();
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
    }
}
