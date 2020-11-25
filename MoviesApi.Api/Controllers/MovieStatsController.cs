using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Api.Data;
using MoviesApi.Api.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi.Api.Controllers
{
    [Route("api/movies/stats")]
    [ApiController]
    public class MovieStatsController:ControllerBase
    {
        private readonly IMoviesRepo _repository;
        private readonly IMapper _mapper;

        public MovieStatsController(IMoviesRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieStatDto>> GetMovieStats()

        {
            var movieStats = _repository.GetMoviesStatics().ToList();

            if (movieStats.Count > 0)
            {

                return Ok(_mapper.Map<IEnumerable<MovieStatDto>>(movieStats));
            }

            return NotFound();

        }
    }
}
