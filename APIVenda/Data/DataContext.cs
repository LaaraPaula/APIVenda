using ApiVenda.Models;
using APIVenda.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiVenda.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }

    }
}
