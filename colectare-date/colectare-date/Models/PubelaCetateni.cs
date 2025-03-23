using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace colectare_date.Models
{
    public class PubelaCetateni
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Pubela")]
        public string PubelaId { get; set; }
        public Pubela? Pubela { get; set; }

        [Required]
        public int CetateanId { get; set; }
        public Cetatean? Cetatean { get; set; }  

        [Required]
        public string Adresa { get; set; }
    }
}
