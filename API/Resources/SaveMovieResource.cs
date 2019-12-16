using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_project.API.Resources
{
    public class SaveMovieResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string imdbID { get; set; }

        [Required]
        [ForeignKey("MovieList")]
        public int MovieListRefId { get; set; }
    }
}
