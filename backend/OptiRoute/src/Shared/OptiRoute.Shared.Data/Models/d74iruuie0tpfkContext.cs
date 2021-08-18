using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OptiRoute.Shared.Data.Models
{
    public partial class d74iruuie0tpfkContext : DbContext
    {
        public d74iruuie0tpfkContext()
        {
        }

        public d74iruuie0tpfkContext(DbContextOptions<d74iruuie0tpfkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("test");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
