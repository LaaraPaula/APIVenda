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
            builder.Entity<Vendas>()
                   .HasOne(venda => venda.Funcionario)
                   .WithMany(funcionario => funcionario.Vendas)
                   .HasForeignKey(venda => venda.FuncionarioId).IsRequired(false);

            builder.Entity<Vendas>()
                   .HasOne(venda => venda.Cliente)
                   .WithMany(cliente => cliente.Vendas)
                   .HasForeignKey(venda => venda.ClienteId).IsRequired(false); 
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Vendas> Vendas { get; set; }

    }
}
