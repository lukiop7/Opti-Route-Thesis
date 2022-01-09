using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptiRoute.Domain.Entities;


namespace OptiRoute.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.DbId);

            builder.HasOne(x => x.Route)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.RouteDbId)
                .IsRequired();

            builder.Ignore(x => x.DepotDistanceFrom);

            builder.Ignore(x => x.DepotDistanceTo);

            builder.Ignore(x => x.DepotTimeFrom);

            builder.Ignore(x => x.DepotTimeTo);
                
        }
    }
}
