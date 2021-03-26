using Microsoft.EntityFrameworkCore;
using Siagri.Clima.Business.Entities;

namespace Siagri.Clima.Data.Context
{
    public class SiagriContext : DbContext
    {
        public SiagriContext(DbContextOptions<SiagriContext> options) : base(options) { }

        public DbSet<Localidade> Localidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Localidade>().HasKey(l => l.LocalidadeId);

            modelBuilder.Entity<Localidade>().HasIndex(l => l.LocalidadeId);

            modelBuilder.Entity<Localidade>().OwnsOne(l => l.Cidade, c =>
            {
                c.Property(a => a.Nome)
                    .HasColumnName("Nome");
            });

            modelBuilder.Entity<Localidade>().OwnsOne(l => l.Coordenada, c =>
            {
                c.Property(a => a.Longitude)
                    .HasColumnName("Longitude");

                c.Property(a => a.Latitude)
                    .HasColumnName("Latitude");
            });
        }
    }
}
