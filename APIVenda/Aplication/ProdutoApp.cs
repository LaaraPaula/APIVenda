using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Produto;
using System.Collections.Generic;
using APIVenda.Data.Dtos.Estoque;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Utilitarios;

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
            Validacoes.ValidarCampo(produtoDto.Nome, "nome");
            Validacoes.ValidarCampo(produtoDto.Descricao, "descrição");
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
                return produtoDto;
            }
            produto = _produtoRepository.GetProdutoId(produtoDto.Id);
            Validacoes.ValidaPesquisa(produto, "Produto");

            produto.Nome = produtoDto.Nome;
            produto.Descricao = produtoDto.Descricao;
            produto.PrecoUnitario = produtoDto.PrecoUnitario;

            _produtoRepository.UpdateProduto(produto);

            return produtoDto;
        }

        public string DeletaProduto(int id)
        {
            var produto = _produtoRepository.GetProdutoId(id);
            Validacoes.ValidaPesquisa(produto, "Produto");

            var estoque = _controleEstoqueRepository.GetFornecedorEstoque(produto.Id);
            Validacoes.ValidaDeletaComRelacionamento(estoque, "Produto", "estoque");

            var nome = produto.Nome;
            _produtoRepository.Excluir(produto);
            return nome;
        }

        public ProdutoDto ExibePorId(int id)
        {
            var produto = _produtoRepository.GetProdutoId(id);
            Validacoes.ValidaPesquisa(produto, "Produto");

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
            var produto = _produtoRepository.GetProdutoId(estoque.ProdutoId);
            Validacoes.ValidaPesquisa(produto, "Produto");

            var fornecedor = _fornecedorRepository.GetFornecedorId(estoque.FornecedorId);
            Validacoes.ValidaPesquisa(fornecedor, "Fornecedor");

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
        public IList<ExibeEstoqueDto> ExibeEstoque(int dias)
        {
            var estoque = _controleEstoqueRepository.EstoqueData(dias);
            return estoque;
        }
    }
}
