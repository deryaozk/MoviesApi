using MoviesApi.Api.Dtos;
using MoviesApi.Api.Models;
using System.Collections.Generic;

namespace MoviesApi.Api.Data
{
    public interface IMoviesRepo
    {
        IList<Movie> GetMoviesByMovieId(int id);

        IEnumerable<MovieStatDto> GetMoviesStatics();

        void CreateMovie(Movie movie);
    }
}
