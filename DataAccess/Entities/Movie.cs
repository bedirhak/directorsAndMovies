#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DataAccess.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ReleaseYear { get; set; }

        public decimal? ImdbRank { get; set; }

        public int DirectorId { get; set; }

        public Director Director { get; set; }

        public List <MovieGenre> MoviesGenres { get; set; }
    }
}
