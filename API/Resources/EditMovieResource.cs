using System.ComponentModel.DataAnnotations;

namespace movie_project.API.Resources
{
	public class EditMovieResource
	{
	    [Required]
		public int Watched { get; set; }
	}
}
