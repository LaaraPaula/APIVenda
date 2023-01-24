using APIVenda.Data.Dtos.Cliente;
using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Produto;
using System.Collections;
using System.Collections.Generic;

namespace APIVenda.Aplication
{
    public class ProdutoApp
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoApp(DataContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
        }

        public ProdutoDto SaveProduto(ProdutoDto produtoDto)
        {
            if (string.IsNullOrEmpty(produtoDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(produtoDto.Descricao)) throw new Exception("Necessário preencher o campo descrição");
            if (produtoDto.PrecoUnitario<=0) throw new Exception("Campo PreçoUnitario deve ser preenchido com valor maior que '0' ");
            if (produtoDto.QuantidadeEstoque<=0) throw new Exception("Campo QuantidadeEstoque deve ser preenchido com valor maior que '0' ");

            Produto produto;
            if (produtoDto.Id == 0)
            {

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
                produto.QuantidadeEstoque = produtoDto.QuantidadeEstoque;

                _produtoRepository.UpdateProduto(produto);
            }

            return produtoDto;
        }

        public void DeletaProduto(int id)
        {
            var produto = _produtoRepository.GetProdutoId(id) ?? throw new Exception("Produto não encontrado");
            _produtoRepository.Excluir(produto);
        }

        public ProdutoDto ExibePorId(int id)
        {
            var produto = _produtoRepository.GetProdutoId(id) ?? throw new Exception("Produto não encontrado");

            return new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                PrecoUnitario = produto.PrecoUnitario,
                QuantidadeEstoque = produto.QuantidadeEstoque
            };
        }

        public IList<ProdutoDto> ExibeProdutos()
        {
            var produto = _produtoRepository.GetProdutos();
            return produto;
        }
    }
}
