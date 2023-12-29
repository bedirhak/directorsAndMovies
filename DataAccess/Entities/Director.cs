#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        public List<Movie> Movie { get; set; }
    }
}
