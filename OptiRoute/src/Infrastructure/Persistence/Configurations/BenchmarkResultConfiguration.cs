using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptiRoute.Domain.Entities;

namespace OptiRoute.Infrastructure.Persistence.Configurations
{
    public class BenchmarkResultConfiguration : IEntityTypeConfiguration<BenchmarkResult>
    {
        public void Configure(EntityTypeBuilder<BenchmarkResult> builder)
        {
            builder.HasKey(x => x.DbId);

            builder.Property(x => x.DbId)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.BenchmarkInstance)
       .WithOne(p => p.BenchmarkResult)
       .HasForeignKey<BenchmarkResult>(d => d.BenchmarkInstanceDbId);

            builder.HasOne(d => d.BestSolution)
                .WithOne(p => p.BestBenchmarkResult)
                .HasForeignKey<BenchmarkResult>(d => d.BestSolutionDbId);

            builder.HasOne(d => d.Solution)
                .WithOne(p => p.BenchmarkResult)
                .HasForeignKey<BenchmarkResult>(d => d.SolutionDbId);
        }
    }
}