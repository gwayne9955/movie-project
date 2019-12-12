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

        public async Task<SaveMovieListResponse> SaveAsync(MovieList movieList)
        {
            try
            {
                await _movieListRepository.AddAsync(movieList);
                await _unitOfWork.CompleteAsync();

                return new SaveMovieListResponse(movieList);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveMovieListResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }
    }
}
