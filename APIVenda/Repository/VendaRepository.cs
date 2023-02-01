using APIVenda.Data;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Repository
{
    public class VendaRepository
    {
        private DataContext _context;
        private PedidoRepository _pedidoRepository;
        public VendaRepository(DataContext context)
        {
            _context = context;
        }

        public int AddVenda(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return venda.Id;
        }

        public void Excluir(object pedido)
        {
            _context.Remove(pedido);
            _context.SaveChanges();
        }

        public Venda GetVendaId(int id)
        {
            return _context.Vendas.FirstOrDefault(x => x.Id == id);
        }

        public IList<ExibeVendaDto> GetVendas()
        {
            var query = from ved in _context.Vendas
                        join fun in _context.Funcionarios
                        on ved.FuncionarioId equals fun.Id
                        join cli in _context.Clientes
                        on ved.ClienteId equals cli.Id
                        select new ExibeVendaDto
                        {
                            Id = ved.Id,
                            ClientePedido = cli.Nome,
                            FuncionarioPedido = fun.Nome,
                            ValorCompra = ved.ValorFinal
                        };
            return query.ToList();
        }

        public Venda UpdateVenda(Venda venda)
        {
            _context.Vendas.Update(venda);
            _context.SaveChanges();
            return venda;
        }

        public Venda GetVendasId(int id)
        {
            return _context.Vendas.FirstOrDefault(x => x.Id == id);
        }

        public ExibeVendaDto ExibeVenda(int id)
        {
            var query = from ved in _context.Vendas
                        join fun in _context.Funcionarios
                        on ved.FuncionarioId equals fun.Id
                        join cli in _context.Clientes
                        on ved.ClienteId equals cli.Id
                        where ved.Id ==id 
                        select new ExibeVendaDto
                        {
                            ClientePedido = cli.Nome,
                            FuncionarioPedido = fun.Nome,
                            ValorCompra = ved.ValorFinal
                        };
            return query.FirstOrDefault();
        }

        public IList<ExibeVendaDto> VendaData(int dias)
        {
            var data = DateTime.Now.AddDays(-dias);


            var vendas = _context.Vendas.Where(p => p.DataVenda >= data);
            var query = from ved in _context.Vendas
                        join fun in _context.Funcionarios
                        on ved.FuncionarioId equals fun.Id
                        join cli in _context.Clientes
                        on ved.ClienteId equals cli.Id
                        where ved.DataVenda>=data
                        select new ExibeVendaDto
                        {
                            DataVenda = ved.DataVenda,
                            ClientePedido = cli.Nome,
                            FuncionarioPedido = fun.Nome,
                            ValorCompra = ved.ValorFinal
                        };
            return query.ToList();
        }

        public Venda ObterClienteVenda(int id)
        {
            var cliente = _context.Vendas.FirstOrDefault(x => x.ClienteId == id);
            return cliente;
        }

        public Venda ObterFuncionarioVenda(int id)
        {
            var funcionario = _context.Vendas.FirstOrDefault(x => x.FuncionarioId == id);
            return funcionario;
        }
    }
}
