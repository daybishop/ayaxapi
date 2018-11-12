using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AyaxApi.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Division> Divisions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RealtorTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DivisionTypeConfiguration());
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Data Source=ayax.db");
        // }
    }
    class RealtorTypeConfiguration : IEntityTypeConfiguration<Realtor>
    {
        public void Configure(EntityTypeBuilder<Realtor> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(value => value.Firstname).HasMaxLength(250);
            builder.Property(value => value.Lastname).HasMaxLength(250);
            builder.Property(value => value.CreatedDateTime)
                .IsRequired()
                .HasColumnType("DateTime");
        }
    }
    class DivisionTypeConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(value => value.Name).HasMaxLength(200);
            builder.Property(value => value.CreatedDateTime)
                .IsRequired()
                .HasColumnType("DateTime"); ;
        }
    }

}