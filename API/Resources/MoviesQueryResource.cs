using System;
namespace movie_project.API.Resources
{
    public class MoviesQueryResource : QueryResource
    {
        public string? ApplicationUserRefId { get; set; }
    }
}
