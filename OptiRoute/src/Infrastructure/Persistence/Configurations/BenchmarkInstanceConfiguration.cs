using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptiRoute.Domain.Entities;

namespace OptiRoute.Infrastructure.Persistence.Configurations
{
    public class BenchmarkInstanceConfiguration : IEntityTypeConfiguration<BenchmarkInstance>
    {
        public void Configure(EntityTypeBuilder<BenchmarkInstance> builder)
        {
            builder.HasKey(x => x.DbId);

            builder.Property(x => x.DbId)
           .ValueGeneratedOnAdd();

            builder.Property(x => x.BestDistance)
                .IsRequired();

            builder.Property(x => x.BestVehicles)
               .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}