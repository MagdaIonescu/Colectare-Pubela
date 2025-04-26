using colectare_date.Models;

namespace colectare_date.ViewModels
{
    public class RutaOptimizataViewModel
    {
        public List<Colectare> Colectari { get; set; } 
        public List<Colectare> ColectariOptimizate { get; set; }
        public int DistantaTraseuNeoptimizat { get; set; }
        public int DistantaTraseuOptimizat { get; set; }
        public int DiferentaDistanta { get; set; }
        public int TimpEstimativ { get; set; }
    }
}
