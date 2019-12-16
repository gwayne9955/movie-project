using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_project.Models
{
    public class Movie
    {
        [Key, Column(Order = 1, TypeName = "varchar(255)")]
        [Required]
        //[Column(TypeName = "varchar(255)")]
        public string imdbID { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        [ForeignKey("MovieList")]
        public int MovieListRefId { get; set; }
        public MovieList MovieList { get; set; }
    }
}
