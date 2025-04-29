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
        public string Adresa { get; set; }
        public float Longitudine { get; set; }
        public float Latitudine { get; set; }
        public bool EsteRezolvata { get; set; } = false;
    }
}
