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

            builder.HasOne(x => x.BenchmarkInstance)
                .WithOne(x => x.BenchmarkResult)
                .HasForeignKey<BenchmarkResult>(x => x.BenchmarkInstanceDbId)
                .IsRequired();

            builder.HasOne(x => x.Solution)
               .WithOne(x => x.BenchmarkResult)
               .HasForeignKey<BenchmarkResult>(x => x.SolutionDbId)
               .IsRequired();

            builder.HasOne(x => x.BestSolution)
             .WithOne(x => x.BestBenchmarkResult);
        }
    }
}
