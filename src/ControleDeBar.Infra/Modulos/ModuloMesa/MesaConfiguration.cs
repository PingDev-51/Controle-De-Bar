using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Modulos.ModuloMesa;

public sealed class MesaConfiguration : IEntityTypeConfiguration<Mesa>
{

    public void Configure(EntityTypeBuilder<Mesa> builder)
    {
        builder.ToTable("TBMesa");

        builder.HasKey(m => m.Id)
            .HasName("PK_TBMesa");

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.UserId)
            .IsRequired();

        builder.Property(m => m.NumeroDaMesa)
            .IsRequired();

        builder.Property(m => m.QuantidadeDeLugares)
            .HasMaxLength(100)
           .IsRequired();
        // builder.HasIndex(m => new { m.UserId, m.Nome })
        //     .IsUnique()
        //     .HasDatabaseName("UQ_TBMesa_UserId_Nome");
    }

}
