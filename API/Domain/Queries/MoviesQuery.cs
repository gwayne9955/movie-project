using System;
namespace movie_project.API.Domain.Queries
{
    public class MoviesQuery : Query
    {
        public string? ApplicationUserRefId { get; set; }

        public MoviesQuery(string? applicationUserRefId) : base(1, 1000)
        {
            ApplicationUserRefId = applicationUserRefId;
        }
    }
}
