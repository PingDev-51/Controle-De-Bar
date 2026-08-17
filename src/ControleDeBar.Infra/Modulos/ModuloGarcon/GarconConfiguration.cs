using System;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Modulos.ModuloGarcon;

public class GarconConfiguration : IEntityTypeConfiguration<Garcon>
{
    public void Configure(EntityTypeBuilder<Garcon> builder)
    {
        builder.ToTable("TBGarcon");

        builder.HasKey(g => g.Id)
            .HasName("PK_TBGarcon");

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property(g => g.UserId)
            .IsRequired();

        builder.Property(g => g.Nome)
            .HasMaxLength(100)
            .IsRequired();


        builder.HasIndex(g => new { g.UserId, g.Nome })
            .IsUnique()
            .HasDatabaseName("UQ_TBGarcon_UserId_Nome");
    }
}
