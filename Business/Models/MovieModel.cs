#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Movie Name")]
        public string Name { get; set; }
        
        [DisplayName("Release Year")]
        public int? ReleaseYear { get; set; }

        [DisplayName("Imdb Rank")]
        public decimal? ImdbRank { get; set; }

        [DisplayName("Director Id")]
        public int DirectorId { get; set; }


        [DisplayName("Director Name")]
        public string DirectorOutput { get; set; }
    }
}
