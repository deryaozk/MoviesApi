using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Api.Dtos
{
    public class MovieReadDto
    {
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
