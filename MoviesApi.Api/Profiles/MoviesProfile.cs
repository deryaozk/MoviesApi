using AutoMapper;
using MoviesApi.Api.Dtos;
using MoviesApi.Api.Models;

namespace MoviesApi.Api.Profiles
{
    public class MoviesProfile:Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movie, MovieReadDto>();
            CreateMap<MovieCreateDto, Movie>();
        }
    }
}
