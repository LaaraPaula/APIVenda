using APIVenda.Data;
using APIVenda.Data.Dtos.Estoque;
using APIVenda.Models;
using System;
using System.Linq;

namespace APIVenda.Repository
{
    public class ControleEstoqueRepository
    {
        private DataContext _context;

        public ControleEstoqueRepository(DataContext context)
        {
            _context = context;
        }

        public void SaveEstoque(ControladorEstoque estoque)
        {
            _context.Estoques.Add(estoque);
            _context.SaveChanges();
        }

        public ControladorEstoque GetFornecedorEstoque(int id)
        {
            var fornecedor = _context.Estoques.FirstOrDefault(x => x.FornecedorId == id);
            return fornecedor;
        }
    }
}
