namespace movie_project.API.Domain.Queries
{
    public class MovieListsQuery : Query
    {
        public string? ApplicationUserRefId { get; set; }

        public MovieListsQuery(string? applicationUserRefId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            ApplicationUserRefId = applicationUserRefId;
        }
    }
}
