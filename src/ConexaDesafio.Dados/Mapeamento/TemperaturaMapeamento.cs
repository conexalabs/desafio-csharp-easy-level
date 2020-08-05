using ConexaDesafio.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConexaTreino.DATA.Mapeamento
{
    public class TemperaturaMapeamento : IEntityTypeConfiguration<Temperatura>
    {
        public void Configure(EntityTypeBuilder<Temperatura> builder)
        {
            builder.ToTable("Temperatura");

            builder.HasKey(a => a.TemperaturaId);

            builder.Property(a => a.TemperaturaCelsius)
                .IsRequired()
                .HasColumnType("varchar(6)");

            builder.Property(a => a.CidadeId).HasColumnType("int");

            builder.Property(a => a.Data)
                .HasColumnType("date");
        }
    }
}
