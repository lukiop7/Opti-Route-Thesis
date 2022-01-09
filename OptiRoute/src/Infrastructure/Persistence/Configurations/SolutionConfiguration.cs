using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptiRoute.Domain.Entities;


namespace OptiRoute.Infrastructure.Persistence.Configurations
{
   public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(x => x.DbId);

            builder.HasOne(x => x.Depot)
                .WithOne(x => x.Solution)
                .HasForeignKey<Solution>(x => x.DepotDbId)
                .IsRequired();
        }
    }
}
