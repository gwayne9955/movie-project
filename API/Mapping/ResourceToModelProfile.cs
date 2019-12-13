using AutoMapper;
using movie_project.Models;
using movie_project.API.Resources;
using movie_project.API.Domain.Queries;

namespace movie_project.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveMovieListResource, MovieList>();
            CreateMap<MovieListsQueryResource, MovieListsQuery>();
        }
    }
}
