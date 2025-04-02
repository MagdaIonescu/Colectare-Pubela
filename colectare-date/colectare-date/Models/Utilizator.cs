using System.ComponentModel.DataAnnotations;

namespace colectare_date.Models
{
    public class Utilizator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Parola { get; set; }
    }
}
