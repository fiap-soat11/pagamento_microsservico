using System;
using System.Collections.Generic;
using DataSource;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataSource.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<StatusPagamento> StatusPagamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<FormaPagamento>(entity =>
        {
            entity.HasKey(e => e.IdFormaPagamento).HasName("PRIMARY");

            entity.ToTable("Forma_pagamento");

            entity.Property(e => e.IdFormaPagamento).HasColumnName("id_forma_pagamento");
            entity.Property(e => e.Ativo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.IdPagamento).HasName("PRIMARY");

            entity.ToTable("Pagamento");

            entity.HasIndex(e => e.IdFormaPagamento, "id_forma_pagamento");

            entity.HasIndex(e => e.IdPedido, "id_pedido");

            entity.HasIndex(e => e.IdStatusPagamento, "id_status_pagamento");
            entity.HasIndex(e => e.Tentativa, "tentativa");

            entity.Property(e => e.IdPagamento).HasColumnName("id_pagamento");
            entity.Property(e => e.DataPagamento)
                .HasColumnType("datetime")
                .HasColumnName("data_pagamento");
            entity.Property(e => e.IdFormaPagamento).HasColumnName("id_forma_pagamento");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdStatusPagamento).HasColumnName("id_status_pagamento");
            entity.Property(e => e.ValorPago)
                .HasPrecision(10, 2)
                .HasColumnName("valor_pago");

            entity.HasOne(d => d.IdFormaPagamentoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdFormaPagamento)
                .HasConstraintName("Pagamento_ibfk_2");

            entity.HasOne(d => d.IdStatusPagamentoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdStatusPagamento)
                .HasConstraintName("Pagamento_ibfk_3");
        });

        modelBuilder.Entity<StatusPagamento>(entity =>
        {
            entity.HasKey(e => e.IdStatusPagamento).HasName("PRIMARY");

            entity.ToTable("Status_Pagamento");

            entity.Property(e => e.IdStatusPagamento).HasColumnName("id_status_pagamento");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
