using System;
using movie_project.Models;

namespace movie_project.API.Domain.Services.Communication
{
    public class MovieResponse : BaseResponse
    {
        public Movie Movie { get; private set; }

        private MovieResponse(bool success, string message, Movie movie) : base(success, message)
        {
            Movie = movie;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="movie">Saved movieList.</param>
        /// <returns>Response.</returns>
        public MovieResponse(Movie movie) : this(true, string.Empty, movie)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public MovieResponse(string message) : this(false, message, null)
        { }
    }
}
