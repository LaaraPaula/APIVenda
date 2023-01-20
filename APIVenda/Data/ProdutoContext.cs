using ApiVenda.Models;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Data
{
    public class ProdutoContext :DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext>opt) : base (opt)
        {

        }
        public DbSet<Produto> Produtos { get; set; }
    }
}
