using System;
using movie_project.Models;
namespace movie_project.API.Domain.Services.Communication
{
    public class SaveMovieListResponse : BaseResponse
    {
        public MovieList MovieList { get; private set; }

        private SaveMovieListResponse(bool success, string message, MovieList movieList) : base(success, message)
        {
            MovieList = movieList;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="movieList">Saved movieList.</param>
        /// <returns>Response.</returns>
        public SaveMovieListResponse(MovieList movieList) : this(true, string.Empty, movieList)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveMovieListResponse(string message) : this(false, message, null)
        { }
    }
}
