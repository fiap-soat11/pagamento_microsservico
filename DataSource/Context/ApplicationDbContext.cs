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

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<FormaPagamento> FormaPagamentos { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoProduto> PedidoProdutos { get; set; }

    public virtual DbSet<Preparo> Preparos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<ProdutoIngrediente> ProdutoIngredientes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StatusPagamento> StatusPagamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Cpf).HasName("PRIMARY");

            entity.ToTable("Cliente");

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.Ativo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

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

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("PRIMARY");

            entity.ToTable("Ingrediente");

            entity.Property(e => e.IdIngrediente).HasColumnName("id_ingrediente");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.EstoqueMinimo)
                .HasPrecision(10, 2)
                .HasColumnName("estoque_minimo");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.PrecoUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("preco_unitario");
            entity.Property(e => e.QuantidadeEmEstoque)
                .HasPrecision(10, 2)
                .HasColumnName("quantidade_em_estoque");
            entity.Property(e => e.UnidadeMedida)
                .HasMaxLength(20)
                .HasColumnName("unidade_medida");
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

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pagamento_ibfk_1");

            entity.HasOne(d => d.IdStatusPagamentoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdStatusPagamento)
                .HasConstraintName("Pagamento_ibfk_3");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PRIMARY");

            entity.ToTable("Pedido");

            entity.HasIndex(e => e.Cpf, "cpf");

            entity.HasIndex(e => e.IdStatusAtual, "id_status_atual");

            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.DataPedido).HasColumnName("data_pedido");
            entity.Property(e => e.IdStatusAtual).HasColumnName("id_status_atual");
            entity.Property(e => e.ValorTotal)
                .HasPrecision(10, 2)
                .HasColumnName("valor_total");

            entity.HasOne(d => d.CpfNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Cpf)
                .HasConstraintName("Pedido_ibfk_1");

            entity.HasOne(d => d.IdStatusAtualNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdStatusAtual)
                .HasConstraintName("Pedido_ibfk_2");
        });

        modelBuilder.Entity<PedidoProduto>(entity =>
        {
            entity.HasKey(e => e.IdPedidoProduto).HasName("PRIMARY");

            entity.ToTable("Pedido_Produto");

            entity.HasIndex(e => e.IdPedido, "id_pedido");

            entity.HasIndex(e => e.IdProduto, "id_produto");

            entity.Property(e => e.IdPedidoProduto).HasColumnName("id_pedido_produto");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProduto).HasColumnName("id_produto");
            entity.Property(e => e.Observacao)
                .HasMaxLength(200)
                .HasColumnName("observacao");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoProdutos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pedido_Produto_ibfk_1");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.PedidoProdutos)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pedido_Produto_ibfk_2");
        });

        modelBuilder.Entity<Preparo>(entity =>
        {
            entity.HasKey(e => e.IdPreparo).HasName("PRIMARY");

            entity.ToTable("Preparo");

            entity.HasIndex(e => e.IdPedido, "id_pedido");

            entity.HasIndex(e => e.IdStatus, "id_status");

            entity.Property(e => e.IdPreparo).HasColumnName("id_preparo");
            entity.Property(e => e.DataStatus)
                .HasColumnType("datetime")
                .HasColumnName("data_status");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Preparos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Preparo_ibfk_1");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Preparos)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Preparo_ibfk_2");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PRIMARY");

            entity.ToTable("Produto");

            entity.HasIndex(e => e.IdCategoria, "id_categoria");

            entity.Property(e => e.IdProduto).HasColumnName("id_produto");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Imagens)
                .HasMaxLength(500)
                .HasColumnName("imagens");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Produto_ibfk_1");
        });

        modelBuilder.Entity<ProdutoIngrediente>(entity =>
        {
            entity.HasKey(e => e.IdProdutoIngrediente).HasName("PRIMARY");

            entity.ToTable("Produto_Ingrediente");

            entity.HasIndex(e => e.IdIngrediente, "id_ingrediente");

            entity.HasIndex(e => e.IdProduto, "id_produto");

            entity.Property(e => e.IdProdutoIngrediente).HasColumnName("id_produto_ingrediente");
            entity.Property(e => e.IdIngrediente).HasColumnName("id_ingrediente");
            entity.Property(e => e.IdProduto).HasColumnName("id_produto");
            entity.Property(e => e.Quantidade)
                .HasPrecision(10, 2)
                .HasColumnName("quantidade");

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.ProdutoIngredientes)
                .HasForeignKey(d => d.IdIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Produto_Ingrediente_ibfk_1");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.ProdutoIngredientes)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Produto_Ingrediente_ibfk_2");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
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
