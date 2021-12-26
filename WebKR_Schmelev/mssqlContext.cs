using Microsoft.EntityFrameworkCore;

namespace WebKR_Schmelev
{
    public partial class mssqlContext : DbContext
    {
        public mssqlContext() { }

        public mssqlContext(DbContextOptions<mssqlContext> options)
            : base(options) { }

        public virtual DbSet<MssqlTest> MssqlTests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<MssqlTest>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("MSSQL_Test");

                entity.Property(e => e.TestId)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Test_ID");

                entity.Property(e => e.TestText)
                    .HasColumnType("text")
                    .HasColumnName("Test_Text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
