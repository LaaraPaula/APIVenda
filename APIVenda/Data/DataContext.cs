using APIVenda.Models;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pedido>()
                   .HasOne(venda => venda.Funcionario)
                   .WithMany(funcionario => funcionario.Vendas)
                   .HasForeignKey(venda => venda.FuncionarioId).IsRequired(false);

            builder.Entity<Pedido>()
                   .HasOne(venda => venda.Cliente)
                   .WithMany(cliente => cliente.Vendas)
                   .HasForeignKey(venda => venda.ClienteId).IsRequired(false);

            builder.Entity<Pedido>()
                   .HasOne(pedido => pedido.Produto)
                   .WithMany(produto => produto.Pedidos)
                   .HasForeignKey(pedido => pedido.ProdutoId).IsRequired(false);

            builder.Entity<Pedido>()
                   .HasOne(pedido => pedido.Venda)
                   .WithMany(venda => venda.Pedidos)
                   .HasForeignKey(pedido => pedido.VendaId).IsRequired(false);
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Pedido> Pedidos{ get; set; }
        public DbSet<Venda> Vendas { get; set; }

    }
}
