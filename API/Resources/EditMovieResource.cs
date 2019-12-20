using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_project.API.Resources
{
	public class EditMovieResource
	{
	    [Required]
		public int Watched { get; set; }
	}
}
