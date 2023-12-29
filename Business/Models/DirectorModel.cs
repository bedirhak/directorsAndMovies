#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DirectorModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }


        public string FullName => $"{Name} {Surname}";

    }
}
