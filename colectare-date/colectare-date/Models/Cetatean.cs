using System.ComponentModel.DataAnnotations;

namespace colectare_date.Models
{
    public class Cetatean
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nume { get; set; }
        [Required]
        public string Prenume { get; set; }

        [Required]
        [StringLength(13,MinimumLength =13,ErrorMessage = "Un CNP valid are exact 13 caractere!")]
        public string CNP { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
