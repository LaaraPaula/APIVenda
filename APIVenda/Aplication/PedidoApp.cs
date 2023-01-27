using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            pedidoDto.ValorTotalPedido = pedidoDto.QuantidadeItens * produto.PrecoUnitario;
            pedidoDto.ValorUnitario = produto.PrecoUnitario;

            Pedido pedido;
            if (pedidoDto.Id == 0)
            {

                
                pedido = new Pedido
                {
                    ProdutoId = pedidoDto.ProdutoId,
                    VendaId = pedidoDto.VendaId,
                    ValorUnitario = produto.PrecoUnitario,
                    QuantidadeItens = pedidoDto.QuantidadeItens,
                    ValorTotalPedido = pedidoDto.ValorTotalPedido
                };
                pedidoDto.Id = _pedidoRepository.AddPedido(pedido);

                _produtoRepository.AtualizaEstoque(pedidoDto.Id, produto.Id);
            }
            else
            {
                pedido = _pedidoRepository.GetPedidoId(pedidoDto.Id) ?? throw new Exception("Pedido não encontrado");

                _produtoRepository.AtualizaEstoqueUpdate(pedido.Id, produto.Id);

                pedido.ProdutoId = pedidoDto.ProdutoId;
                pedido.ValorUnitario = produto.PrecoUnitario;
                pedido.QuantidadeItens = pedidoDto.QuantidadeItens;
                pedido.ValorTotalPedido = pedidoDto.ValorTotalPedido;

                _pedidoRepository.UpdatePedido(pedido);
                _produtoRepository.AtualizaEstoque(pedidoDto.Id, produto.Id);
            }

            return pedidoDto;
        }

        public void DeletaPedido(int id)
        {
            var pedido = _pedidoRepository.GetPedidoId(id) ?? throw new Exception("Pedido não encontrado");
            _pedidoRepository.Excluir(pedido);
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
