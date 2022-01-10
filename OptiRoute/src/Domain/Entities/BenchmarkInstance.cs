namespace OptiRoute.Domain.Entities
{
    public class BenchmarkInstance
    {
        public int DbId { get; set; }
        public string Name { get; set; }

        public double BestDistance { get; set; }

        public double BestVehicles { get; set; }

        public BenchmarkResult BenchmarkResult { get; set; }
    }
}