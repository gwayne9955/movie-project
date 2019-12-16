using AutoMapper;
using movie_project.Models;
using movie_project.API.Resources;
using movie_project.API.Domain.Queries;

namespace movie_project.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<MovieList, MovieListResource>();
            CreateMap<QueryResult<MovieList>, QueryResultResource<MovieListResource>>();
            CreateMap<Movie, MovieResource>();
            CreateMap<QueryResult<Movie>, QueryResultResource<MovieResource>>();
        }
    }
}
