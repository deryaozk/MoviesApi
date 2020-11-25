using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Api.Dtos
{
    public class MovieCreateDto
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public int ReleaseYear { get; set; }
    }
}
