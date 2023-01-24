using APIVenda.Data;

namespace APIVenda.Repository
{
    public class PedidoRepository
    {
        private DataContext _context;

        public PedidoRepository(DataContext context)
        {
            _context = context;
        }
    }
}
