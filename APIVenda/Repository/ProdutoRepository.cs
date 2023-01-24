using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Data.Dtos.Produto;
using APIVenda.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Repository
{
    public class ProdutoRepository
    {
        private DataContext _context;

        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public int AddProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto.Id;
        }

        public Produto GetProdutoId(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            return produto;
        }

        public Produto UpdateProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return produto;
        }

        internal void Excluir(Produto produto)
        {
            _context.Remove(produto);
            _context.SaveChanges();
        }

        internal IList<ProdutoDto> GetProdutos()
        {
            var query = from a in _context.Produtos
                        select new ProdutoDto
                        {
                            Nome = a.Nome,
                            Descricao = a.Descricao,
                            PrecoUnitario = a.PrecoUnitario
                        };
            return query.ToList();
        }
    }
}
