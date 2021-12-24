using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebKR_Schmelev
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Test1)
                    .HasName("test_pkey");

                entity.ToTable("test");

                entity.Property(e => e.Test1)
                    .ValueGeneratedNever()
                    .HasColumnName("test1");

                entity.Property(e => e.Test2)
                    .HasMaxLength(40)
                    .HasColumnName("test2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
