using APIVenda.Data;

namespace APIVenda.Repository
{
    public class VendaRepository
    {
        private DataContext _context;

        public VendaRepository(DataContext context)
        {
            _context = context;
        }
    }
}
