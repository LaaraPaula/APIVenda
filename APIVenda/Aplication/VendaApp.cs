using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using APIVenda.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APIVenda.Aplication
{
    public class VendaApp
    {
        private readonly VendaRepository _vendaRepository;
        private readonly PedidoRepository _pedidoRepository;
        private readonly FuncionarioRepository _funcionarioRepository;
        public VendaApp(DataContext context)
        {
            _vendaRepository = new VendaRepository(context);
            _pedidoRepository = new PedidoRepository(context);
            _funcionarioRepository = new FuncionarioRepository(context);
        }

        public VendaDto SaveVenda(VendaDto vendaDto)
        {
            if (vendaDto.FuncionarioId <= 0) throw new Exception("Necessário preencher o campo VendedorId");
            if (vendaDto.ClienteId <= 0) throw new Exception("Necessário preencher o campo ClienteId");

            var pedido = _pedidoRepository.GetPedidosVenda(vendaDto.Id) ?? throw new Exception("Pedidos não encontrados para esta venda");
            var funciorio = _funcionarioRepository.GetVendedor(vendaDto.FuncionarioId) ?? throw new Exception("Vendedor não encontrado");
            Venda venda;
            if (vendaDto.Id == 0)
            {

                venda = new Venda
                {
                    FuncionarioId = vendaDto.FuncionarioId,
                    ClienteId = vendaDto.ClienteId,
                    HorarioVenda = DateTime.Now,
                    ValorFinal = _vendaRepository.SomarPedido(pedido)
                };
                vendaDto.Id = _vendaRepository.AddVenda(venda);
                
            }
            else
            {
                venda = _vendaRepository.GetVendaId(vendaDto.Id) ?? throw new Exception("Venda não encontrada");

                venda.FuncionarioId = vendaDto.FuncionarioId;
                venda.ClienteId = vendaDto.ClienteId;
                venda.ValorFinal = _vendaRepository.SomarPedido(pedido);

                _vendaRepository.UpdateVenda(venda);
            }

            return vendaDto;
        }

        public void DeletaVenda(int id)
        {
            var venda = _vendaRepository.GetVendaId(id) ?? throw new Exception("Venda não encontrada");
            _vendaRepository.Excluir(venda);
        }

        public ExibeVendaDto ExibePorId(int id)
        {
            var venda = _vendaRepository.GetVendaId(id) ?? throw new Exception("Venda não encontrado");

            return _vendaRepository.ExibeVenda(id);
        }

        public IList<ExibeVendaDto> ExibeVendas()
        {
            var vendas = _vendaRepository.GetVendas();
            return vendas;
        }

        public IList<ExibeVendaDto> VendasPorPeriodo(int dias)
        {
            var vendas = _vendaRepository.VendaData(dias);
            return vendas;
        }
    }
}
