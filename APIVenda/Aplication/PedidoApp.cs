using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using APIVenda.Repository;
using APIVenda.Utilitarios;
using System;
using System.Collections.Generic;

namespace APIVenda.Aplication
{
    public class PedidoApp
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly VendaRepository _vendaRepository;
        public PedidoApp(DataContext context)
        {
            _pedidoRepository = new PedidoRepository(context);
            _produtoRepository = new ProdutoRepository(context);
            _vendaRepository = new VendaRepository(context);
        }

        public (PedidoDto,string) SavePedido(PedidoDto pedidoDto)
        {
            var produto = _produtoRepository.GetProdutoId(pedidoDto.ProdutoId) ?? throw new Exception("Produto não encontrado");
            var venda = _vendaRepository.GetVendaId(pedidoDto.VendaId) ?? throw new Exception("Venda não encontrada");
            if (pedidoDto.QuantidadeItens <= 0) throw new Exception("Necessário preencher o campo QuantidadeItens");
                        
            Validacoes.ValidaPesquisa(produto, "Produto");
            Validacoes.ValidaPesquisa(venda, "Venda");

            if (pedidoDto.QuantidadeItens > produto.QuantidadeEstoque) throw new Exception("Quantidade em estoque insuficiente");

            Pedido pedido;
            var tipo = "Editar";
            if (pedidoDto.Id == 0)
            {
                tipo = "Cadastro";
                pedido = new Pedido
                {
                    ProdutoId = pedidoDto.ProdutoId,
                    VendaId = pedidoDto.VendaId,
                    ValorUnitario = produto.PrecoUnitario,
                    QuantidadeItens = pedidoDto.QuantidadeItens,
                    ValorTotalPedido = pedidoDto.QuantidadeItens * produto.PrecoUnitario
                };
                pedidoDto.Id = _pedidoRepository.AddPedido(pedido);

                venda.ValorFinal += pedido.ValorTotalPedido;
                _produtoRepository.AtualizaEstoque(pedidoDto.Id, produto.Id);
                return (pedidoDto,tipo);
            }
            pedido = _pedidoRepository.GetPedidoId(pedidoDto.Id);
            Validacoes.ValidaPesquisa(pedido, "Pedido");

            venda.ValorFinal -= pedido.ValorTotalPedido;

            _produtoRepository.AtualizaEstoqueUpdate(pedido.Id, produto.Id);

            pedido.ProdutoId = pedidoDto.ProdutoId;
            pedido.ValorUnitario = produto.PrecoUnitario;
            pedido.QuantidadeItens = pedidoDto.QuantidadeItens;
            pedido.ValorTotalPedido = pedidoDto.QuantidadeItens * produto.PrecoUnitario;

            _pedidoRepository.UpdatePedido(pedido);
            venda.ValorFinal += pedido.ValorTotalPedido;
            _produtoRepository.AtualizaEstoque(pedidoDto.Id, produto.Id);

            return (pedidoDto,tipo);
        }
        public int DeletaPedido(int id)
        {
            var pedido = _pedidoRepository.GetPedidoId(id);
            Validacoes.ValidaPesquisa(pedido, "Pedido");

            var pedidoId = pedido.Id;
            _pedidoRepository.Excluir(pedido);
            return pedidoId;
        }

        public IList<ExibePedidoDto> ExibePedidos()
        {
            var pedidos = _pedidoRepository.GetPedidos();
            return pedidos;
        }

        public ExibePedidoDto ExibePorId(int id)
        {
            var pedido = _pedidoRepository.GetPedidoId(id);
            Validacoes.ValidaPesquisa(pedido, "Pedido");

            return _pedidoRepository.ExibePedido(id);
        }
    }
}
