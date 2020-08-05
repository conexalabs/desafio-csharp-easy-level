using ConexaDesafio.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConexaDesafio.Dados.Mapeamento
{
    public class CidadeTemperaturaMapeamento : IEntityTypeConfiguration<CidadeTemperatura>
    {
        public void Configure(EntityTypeBuilder<CidadeTemperatura> builder)
        {
            builder.ToTable("Cidade");

            builder.HasKey(f => f.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.Longitude)
                .HasColumnType("int");

            builder.Property(a => a.Latitude)
                .HasColumnType("int");

            builder.HasMany(c => c.Temperatura)
               .WithOne(a => a.Cidade)
               .HasForeignKey(a => a.CidadeId);
        }

    }
}
