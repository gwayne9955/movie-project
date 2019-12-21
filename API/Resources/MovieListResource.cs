namespace movie_project.API.Resources
{
    public class MovieListResource
    {
        public string MovieListId { get; set; }
        public string Name { get; set; }
        public MovieResource[] Movies { get; set; }
    }
}
