using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using APIVenda.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

            _ = _funcionarioRepository.GetVendedor(vendaDto.FuncionarioId) ?? throw new Exception("Vendedor não encontrado");
            Venda venda;
            if (vendaDto.Id == 0)
            {

                venda = new Venda
                {
                    FuncionarioId = vendaDto.FuncionarioId,
                    ClienteId = vendaDto.ClienteId,
                    DataVenda = DateTime.Now
                };
                vendaDto.Id = _vendaRepository.AddVenda(venda); 
                
            }
            else
            {
                venda = _vendaRepository.GetVendaId(vendaDto.Id) ?? throw new Exception("Venda não encontrada");

                venda.FuncionarioId = vendaDto.FuncionarioId;
                venda.ClienteId = vendaDto.ClienteId;

                _vendaRepository.UpdateVenda(venda);
            }

            return vendaDto;
        }

        public int DeletaVenda(int id)
        {
            var venda = _vendaRepository.GetVendaId(id) ?? throw new Exception("Venda não encontrada");
            var vendaId = venda.Id;
            _vendaRepository.Excluir(venda);
            return vendaId;
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

        private decimal CalcularValorVenda(IList<Pedido> pedidos)
        {
            return pedidos.Sum(x => x.ValorTotalPedido);
        }
    }
}
