using System.ComponentModel.DataAnnotations;

namespace colectare_date.Models
{
    public class Colectare
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        public string PubelaId { get; set; }

        [Required]
        public DateTime TimpColectare { get; set; }  
    }
}
