using ControleDeBar.Dominio.Modulos.ModuloPedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeBar.Infra.Modulos.ModuloPedido;

public sealed class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("TBPedidos");

        builder.HasKey(p => p.Id)
            .HasName("PK_TBPedidos");

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.ContaId)
            .IsRequired();

        builder.Property(p => p.ProdutoId)
            .IsRequired();

        builder.Property(p => p.Quantidade)
            .IsRequired();

        builder.Property(p => p.Total)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(p => p.Conta)
            .WithMany()
            .HasForeignKey(p => p.ContaId)
            .HasConstraintName("FK_TBPedidos_TBContas")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Produto)
            .WithMany()
            .HasForeignKey(p => p.ProdutoId)
            .HasConstraintName("FK_TBPedidos_TBProdutos")
            .OnDelete(DeleteBehavior.NoAction);
    }
}