namespace OptiRoute.Domain.Entities
{
    public class BenchmarkResult
    {
        public int DbId { get; set; }

        public int SolutionDbId { get; set; }

        public int BenchmarkInstanceDbId { get; set; }
        public int? BestSolutionDbId { get; set; }

        public Solution Solution { get; set; }

        public Solution BestSolution { get; set; }

        public BenchmarkInstance BenchmarkInstance { get; set; }
    }
}