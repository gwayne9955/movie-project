using System;
namespace movie_project.API.Resources
{
    public class MovieResource
    {
        public string Name { get; set; }
        public string imdbID { get; set; }
        public int MovieListRefId { get; set; }
    }
}
