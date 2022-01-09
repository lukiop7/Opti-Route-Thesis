using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptiRoute.Domain.Entities;


namespace OptiRoute.Infrastructure.Persistence.Configurations
{
   public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(x => x.DbId);

            builder.HasOne(x => x.Depot)
                .WithMany(x => x.Routes)
                .HasForeignKey(x => x.DepotDbId)
                .IsRequired();

            builder.HasOne(x => x.Solution)
               .WithMany(x => x.Routes)
               .HasForeignKey(x => x.SolutionDbId)
               .IsRequired();

            builder.Ignore(x => x.TimeFromDepot);

            builder.Ignore(x => x.TimeToDepot);

            builder.Ignore(x => x.Vehicle);

            builder.Ignore(x => x.WaitingTime);

            builder.Ignore(x => x.CustomersDistance);

            builder.Ignore(x => x.CustomersTime);

            builder.Ignore(x => x.Distances);

            builder.Ignore(x => x.Durations);

        }
    }
}
