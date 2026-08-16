using ControleDeBar.Dominio.Modulos.ModuloContas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Modulos.ModuloContas;

public sealed class ContasConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.ToTable("TBContas");

        builder.HasKey(p => p.Id)
            .HasName("PK_TBContas");

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.NomeCliente)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.DataAbertura)
            .IsRequired();

        builder.Property(p => p.Situacao)
            .IsRequired();

        builder.Property<Guid>("GarconId")
            .IsRequired();

        builder.Property<Guid>("MesaId")
            .IsRequired();

        builder.HasOne(p => p.Garcon)
            .WithMany()
            .HasForeignKey("GarconId")
            .HasConstraintName("FK_TBContas_TBGarcon")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Mesa)
            .WithMany()
            .HasForeignKey("MesaId")
            .HasConstraintName("FK_TBContas_TBMesa")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex("UserId", "GarconId")
            .IsUnique()
            .HasDatabaseName("UQ_TBContas_UserId_Garcon");
    }
}