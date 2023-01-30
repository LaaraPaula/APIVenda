using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Models;
using APIVenda.Repository;
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

        public PedidoDto SavePedido(PedidoDto pedidoDto)
        {
            if (pedidoDto.ProdutoId <= 0) throw new Exception("Necessário preencher o campo ProdutoId");
            if (pedidoDto.VendaId <= 0) throw new Exception("Necessário preencher o campo VendaId");
            if (pedidoDto.QuantidadeItens <= 0) throw new Exception("Necessário preencher o campo QuantidadeItens");

            var produto = _produtoRepository.GetProdutoId(pedidoDto.ProdutoId) ?? throw new Exception("Produto não encontrado");
            if (pedidoDto.QuantidadeItens > produto.QuantidadeEstoque) throw new Exception("Quantidade em estoque insuficiente");

            var venda = _vendaRepository.GetVendaId(pedidoDto.VendaId) ?? throw new Exception("Venda não encontrada");

            Pedido pedido;
            if (pedidoDto.Id == 0)
            {
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
            }
            else
            {
                pedido = _pedidoRepository.GetPedidoId(pedidoDto.Id) ?? throw new Exception("Pedido não encontrado");
                venda.ValorFinal -= pedido.ValorTotalPedido;

                _produtoRepository.AtualizaEstoqueUpdate(pedido.Id, produto.Id);

                pedido.ProdutoId = pedidoDto.ProdutoId;
                pedido.ValorUnitario = produto.PrecoUnitario;
                pedido.QuantidadeItens = pedidoDto.QuantidadeItens;
                pedido.ValorTotalPedido = pedidoDto.QuantidadeItens * produto.PrecoUnitario;

                _pedidoRepository.UpdatePedido(pedido);
                venda.ValorFinal += pedido.ValorTotalPedido;
                _produtoRepository.AtualizaEstoque(pedidoDto.Id, produto.Id);
            }

            return pedidoDto;
        }

        public int DeletaPedido(int id)
        {
            var pedido = _pedidoRepository.GetPedidoId(id) ?? throw new Exception("Pedido não encontrado");
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
            var pedido = _pedidoRepository.GetPedidoId(id) ?? throw new Exception("Pedido não encontrado");

            return _pedidoRepository.ExibePedido(id);
        }
    }
}
