using System;
using AutoMapper;
using movie_project.Models;
using movie_project.API.Resources;

namespace movie_project.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<MovieList, MovieListResource>();
        }
    }
}
