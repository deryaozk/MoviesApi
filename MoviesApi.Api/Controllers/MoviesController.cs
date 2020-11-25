using Microsoft.AspNetCore.Mvc;
using MoviesApi.Api.Models;
using MoviesApi.Api.Data;
using System.Collections.Generic;
using AutoMapper;
using MoviesApi.Api.Dtos;
using System.Linq;

namespace MoviesApi.Api.Controllers
{
    [Route("api/metadata")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepo _repository;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name= "GetMoviesByMovieId")]
        public ActionResult<IEnumerable<MovieReadDto>> GetMoviesByMovieId(int id)
        
        {
            var movieItems = _repository.GetMoviesByMovieId(id);

            if(movieItems.Count>0)
            {
                return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movieItems));
            }

            return NotFound();
            
        }

        [HttpPost]
        public ActionResult<MovieReadDto> CreateMovie(MovieCreateDto movie)
        {
             _repository.CreateMovie(_mapper.Map<Movie>(movie));

            var createdMovie = _mapper.Map<MovieCreateDto>(movie);
            return CreatedAtRoute(nameof(GetMoviesByMovieId), new { Id = createdMovie.MovieId }, createdMovie);
        }
    }
}
