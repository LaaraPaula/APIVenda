using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Produto;
using System.Collections.Generic;
using APIVenda.Data.Dtos.Estoque;

namespace APIVenda.Aplication
{
    public class ProdutoApp
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly FornecedorRepository _fornecedorRepository;
        private readonly PedidoRepository _pedidoRepository;
        private readonly ControleEstoqueRepository _controleEstoqueRepository;

        public ProdutoApp(DataContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
            _fornecedorRepository = new FornecedorRepository(context);
            _pedidoRepository = new PedidoRepository(context);
            _controleEstoqueRepository = new ControleEstoqueRepository(context);
        }

        public ProdutoDto SaveProduto(ProdutoDto produtoDto)
        {
            if (string.IsNullOrEmpty(produtoDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(produtoDto.Descricao)) throw new Exception("Necessário preencher o campo descrição");
            if (produtoDto.PrecoUnitario<=0) throw new Exception("Campo PreçoUnitario deve ser preenchido com valor maior que '0' ");
            

            Produto produto;
            if (produtoDto.Id == 0)
            {
                if (produtoDto.QuantidadeEstoque <= 0) throw new Exception("Campo QuantidadeEstoque deve ser preenchido com valor maior que '0' ");
                produto = new Produto
                {
                    Nome = produtoDto.Nome,
                    Descricao = produtoDto.Descricao,
                    PrecoUnitario = produtoDto.PrecoUnitario,
                    QuantidadeEstoque = produtoDto.QuantidadeEstoque
                };

                produtoDto.Id = _produtoRepository.AddProduto(produto);
            }
            else
            {
                produto = _produtoRepository.GetProdutoId(produtoDto.Id) ?? throw new Exception("Produto não encontrado");

                produto.Nome = produtoDto.Nome;
                produto.Descricao = produtoDto.Descricao;
                produto.PrecoUnitario = produtoDto.PrecoUnitario;

                _produtoRepository.UpdateProduto(produto);
            }

            return produtoDto;
        }

        public string DeletaProduto(int id)
        {
            var produto = _produtoRepository.GetProdutoId(id) ?? throw new Exception("Produto não encontrado");
            var pedido = _pedidoRepository.ObterPedidoProduto(produto.Id);

            if (pedido != null) throw new Exception("Não é possível excluir o produto pois ele pertence a um pedido");
            var nome = produto.Nome;
            _produtoRepository.Excluir(produto);
            return nome;
        }

        public ProdutoDto ExibePorId(int id)
        {
            var produto = _produtoRepository.GetProdutoId(id) ?? throw new Exception("Produto não encontrado");

            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                PrecoUnitario = produto.PrecoUnitario,
                QuantidadeEstoque = produto.QuantidadeEstoque
            };
        }

        public IList<ProdutoDto> ExibeProdutos(string nome)
        {
            var produto = _produtoRepository.GetProdutos(nome);
            return produto;
        }

        public ExibeEstoqueDto AtualizaEstoque(EstoqueDto estoque)
        {
            var produto = _produtoRepository.GetProdutoId(estoque.ProdutoId) ?? throw new Exception("Produto não encontrado");
            var fornecedor = _fornecedorRepository.GetFornecedorId(estoque.FornecedorId) ?? throw new Exception("Fornecedor não encontrado");
            produto.QuantidadeEstoque += estoque.QuantidadeItens;

            _produtoRepository.UpdateProduto(produto);
            var estocado = new ControladorEstoque
            {
                ProdutoId=estoque.ProdutoId,
                FornecedorId=estoque.FornecedorId,
                QuantidadeItens=estoque.QuantidadeItens,
                DataEntradaEstoque=DateTime.Now
            };
            _controleEstoqueRepository.SaveEstoque(estocado);

            return new ExibeEstoqueDto
            {
                Produto = produto.Nome,
                FornecedorProduto=fornecedor.Nome,
                QuantidadeItens=estoque.QuantidadeItens,
                DataEntradaEstoque =estocado.DataEntradaEstoque,
                TotalEstoque=produto.QuantidadeEstoque                
            };
        }
    }
}
