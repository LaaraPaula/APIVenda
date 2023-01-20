using ApiVenda.Models;
using APIVenda.Models;
using Microsoft.EntityFrameworkCore;

namespace APIVenda.Data
{
    public class VendaContext :DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> opt) : base(opt)
        {

        }
        public DbSet<Venda> Vendas { get; set; }
    }
}
