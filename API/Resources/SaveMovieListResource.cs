using System.ComponentModel.DataAnnotations;
namespace movie_project.API.Resources
{
    public class SaveMovieListResource
    {
        [Required]
        public string Name { get; set; }
    }
}
