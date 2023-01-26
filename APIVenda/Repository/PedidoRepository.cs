using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Repository
{
    public class PedidoRepository
    {
        private DataContext _context;

        public PedidoRepository(DataContext context)
        {
            _context = context;
        }

        public int AddPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            return pedido.Id;
        }

        public Pedido GetPedidoId(int id)
        {
            return _context.Pedidos.FirstOrDefault(x => x.Id == id);
        }

        public Pedido UpdatePedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            _context.SaveChanges();
            return pedido;
        }

        public void Excluir(Pedido pedido)
        {
            _context.Remove(pedido);
            _context.SaveChanges();
        }

        public IList<ExibePedidoDto> GetPedidos()
        {
            var query = from ped in _context.Pedidos
                        join pro in _context.Produtos
                        on ped.ProdutoId equals pro.Id
                        select new ExibePedidoDto
                        {
                            ProdutoPedido = pro.Nome,
                            QuantidadeProduto = ped.QuantidadeItens,
                            ValorUnidade = ped.ValorUnitario,
                            ValorPedido = ped.ValorTotalPedido
                        };
            return query.ToList();
        }

        public ExibePedidoDto ExibePedido(int pedidoId)
        {
            var query = from ped in _context.Pedidos
                        join pro in _context.Produtos
                        on ped.ProdutoId equals pro.Id
                        where ped.Id == pedidoId
                        select new ExibePedidoDto
                        {
                            ProdutoPedido = pro.Nome,
                            QuantidadeProduto = ped.QuantidadeItens,
                            ValorUnidade = ped.ValorUnitario,
                            ValorPedido = ped.ValorTotalPedido
                        };
            return query.FirstOrDefault();
        }

        public IList<Pedido> GetPedidosVenda(int id)
        {
           var pedidosVenda = _context.Pedidos.Where(x => x.VendaId == id);
            return pedidosVenda.ToList();
        }
    }
}
