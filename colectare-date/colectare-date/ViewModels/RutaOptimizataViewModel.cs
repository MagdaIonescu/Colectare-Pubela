using colectare_date.Models;

namespace colectare_date.ViewModels
{
    public class RutaOptimizataViewModel
    {
        public List<Colectare> Colectari { get; set; } 
        public List<Colectare> ColectariOptimizate { get; set; }
        public double DistantaTraseuNeoptimizat { get; set; }
        public double DistantaTraseuOptimizat { get; set; }
        public double DiferentaDistanta { get; set; }
        public double TimpEstimativ { get; set; }
    }
}
