using APIVenda.Data;
using APIVenda.Data.Dtos.Estoque;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public IList<ExibeEstoqueDto> EstoqueData(int dias)
        {
            var query = from est in _context.Estoques
                        join pro in _context.Produtos
                        on est.ProdutoId equals pro.Id
                        join forn in _context.Fornecedores
                        on est.FornecedorId equals forn.Id
                        select new ExibeEstoqueDto
                        {
                            DataEntradaEstoque = est.DataEntradaEstoque,
                            Produto = pro.Nome,
                            FornecedorProduto = forn.Nome,
                            QuantidadeItens = est.QuantidadeItens,
                        };
            if (dias != 0)
            {
                var data = DateTime.Now.AddDays(-dias);
                query = query.Where(x => x.DataEntradaEstoque >= data);
            }
            return query.ToList();
        }
    }
}
