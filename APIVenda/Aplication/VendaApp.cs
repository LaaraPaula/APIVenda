using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using APIVenda.Repository;
using APIVenda.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Aplication
{
    public class VendaApp
    {
        private readonly VendaRepository _vendaRepository;
        private readonly ClienteRepository _clienteRepository;
        private readonly FuncionarioRepository _funcionarioRepository;
        private readonly PedidoRepository _pedidoRepository;
        public VendaApp(DataContext context)
        {
            _vendaRepository = new VendaRepository(context);
            _clienteRepository = new ClienteRepository(context);
            _funcionarioRepository = new FuncionarioRepository(context);
            _pedidoRepository = new PedidoRepository(context);
        }

        public VendaDto SaveVenda(VendaDto vendaDto)
        {
            _ = _clienteRepository.GetClienteId(vendaDto.ClienteId) ?? throw new Exception("Cliente não encontrado");

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
                return vendaDto;
            }
            venda = _vendaRepository.GetVendaId(vendaDto.Id);
            Validacoes.ValidaPesquisa(venda, "Venda");

            venda.FuncionarioId = vendaDto.FuncionarioId;
            venda.ClienteId = vendaDto.ClienteId;

            _vendaRepository.UpdateVenda(venda);

            return vendaDto;
        }

        public int DeletaVenda(int id)
        {
            var venda = _vendaRepository.GetVendaId(id);
            Validacoes.ValidaPesquisa(venda, "Venda");

            var vendaId = venda.Id;
            _vendaRepository.Excluir(venda);
            return vendaId;
        }

        public ExibeVendaDto ExibePorId(int id)
        {
            var venda = _vendaRepository.GetVendaId(id);
            Validacoes.ValidaPesquisa(venda, "Venda");

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

        public IList<ExibePedidoDto> GetPedidos(int id)
        {
            var venda = _vendaRepository.GetVendaId(id);
            Validacoes.ValidaPesquisa(venda, "Venda");

            var pedidos = _pedidoRepository.GetPedidosVenda(id);
            return pedidos;
        }
    }
}
