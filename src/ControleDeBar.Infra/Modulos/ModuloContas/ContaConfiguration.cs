using ControleDeBar.Dominio.Modulos.ModuloContas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Modulos.ModuloContas;

public sealed class ContasConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.ToTable("TBContas");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBContas");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.NomeCliente)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.DataAbertura)
            .IsRequired();

        builder.Property(c => c.Situacao)
            .IsRequired();

        builder.HasOne(c => c.Garcon)
            .WithMany()
            .HasForeignKey("GarconId")
            .HasConstraintName("FK_TBContas_TBGarcon")
            .OnDelete(DeleteBehavior.NoAction);
          

        builder.HasOne(c => c.Mesa)
            .WithMany()
            .HasForeignKey("MesaId")
            .HasConstraintName("FK_TBContas_TBMesa")
            .OnDelete(DeleteBehavior.NoAction);
          
    }
}