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
        public PedidoApp(DataContext context)
        {
            _pedidoRepository = new PedidoRepository(context);
            _produtoRepository = new ProdutoRepository(context);
        }

        public PedidoDto SavePedido(PedidoDto pedidoDto)
        {
            if (pedidoDto.ProdutoId <=0) throw new Exception("Necessário preencher o campo ProdutoId");
            if (pedidoDto.VendaId <= 0) throw new Exception("Necessário preencher o campo VendaId");
            if (pedidoDto.ProdutoId <= 0) throw new Exception("Necessário preencher o campo FuncionarioId");
            if (pedidoDto.QuantidadeItens <= 0) throw new Exception("Necessário preencher o campo QuantidadeItens");

            var produto = _produtoRepository.GetProdutoId(pedidoDto.Id) ?? throw new Exception("Produto não encontrado");
            Pedido pedido;
            if (pedidoDto.Id == 0)
            {

                pedido = new Pedido
                {
                    ProdutoId = pedidoDto.ProdutoId,
                    VendaId = pedidoDto.VendaId,
                    ValorUnitario = produto.PrecoUnitario,
                    QuantidadeItens = pedidoDto.QuantidadeItens,
                    ValorTotalPedido = (pedidoDto.QuantidadeItens * produto.PrecoUnitario)
                };
                if (pedido.QuantidadeItens<= produto.QuantidadeEstoque)
                {
                    pedidoDto.Id = _pedidoRepository.AddPedido(pedido);
                }
                else
                {
                    throw new Exception("Quandidade de itens no pedido maior que a quantidade no estoque");
                }
            }
            else
            {
                pedido = _pedidoRepository.GetPedidoId(pedidoDto.Id) ?? throw new Exception("Pedido não encontrado");

                pedido.ProdutoId = pedidoDto.ProdutoId;
                pedido.VendaId = pedidoDto.VendaId;
                pedido.ValorUnitario = produto.PrecoUnitario;
                pedido.QuantidadeItens = pedidoDto.QuantidadeItens;
                pedido.ValorTotalPedido = (pedidoDto.QuantidadeItens * produto.PrecoUnitario);

                _pedidoRepository.UpdatePedido(pedido);
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
