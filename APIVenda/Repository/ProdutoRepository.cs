using APIVenda.Data;
using APIVenda.Data.Dtos.Produto;
using APIVenda.Models;
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

        public void AtualizaEstoque(int pedidoId, int produtoId)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.Id == produtoId);
            var pedido = _context.Pedidos.FirstOrDefault(x => x.Id == pedidoId);
            var estoque = produto.QuantidadeEstoque - pedido.QuantidadeItens;
            produto.QuantidadeEstoque = estoque;
            _context.SaveChanges();
        }

        public void Excluir(Produto produto)
        {
            _context.Remove(produto);
            _context.SaveChanges();
        }

        public IList<ProdutoDto> GetProdutos()
        {
            var query = from a in _context.Produtos
                        select new ProdutoDto
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Descricao = a.Descricao,
                            PrecoUnitario = a.PrecoUnitario,
                            QuantidadeEstoque = a.QuantidadeEstoque
                        };
            return query.ToList();
        }

        public void AtualizaEstoqueUpdate(int pedidoId, int produtoId)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.Id == produtoId);
            var pedido = _context.Pedidos.FirstOrDefault(x => x.Id == pedidoId);
            var renovaEstoque = produto.QuantidadeEstoque + pedido.QuantidadeItens;
            produto.QuantidadeEstoque = renovaEstoque;
            _context.SaveChanges();
        }
    }
}
