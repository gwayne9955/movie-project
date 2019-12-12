using System;
using System.ComponentModel.DataAnnotations;

namespace movie_project.API.Resources
{
    public class MovieListResource
    {
        [Required]
        public string listName { get; set; }
    }
}
