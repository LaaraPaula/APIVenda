using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PedidoController : ControllerBase
    {
        private DataContext _context;
        private IMapper _mapper;
        public PedidoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("AdicionaPedido")]
        public IActionResult AdicionaPedido(CreatePedidoDto dto)
        {
            Pedido pedido = _mapper.Map<Pedido>(dto);
            if (pedido.Id == 0)
            {
                Produto produto = _context.Produtos.FirstOrDefault(p=> p.Id == pedido.ProdutoId);
                if (produto.QuantidadeEstoque >= pedido.QuantidadeItens)
                {
                    pedido.ValorCompra = (pedido.QuantidadeItens * produto.PrecoUnitario);
                    _context.Pedidos.Add(pedido);
                    _context.SaveChanges();
                }
            }
            return CreatedAtAction(nameof(RecuperaPedidoPorId), new { Id = pedido.Id }, pedido);
        }
        [HttpGet("RecuperaPedidoPorId")]
        public IActionResult RecuperaPedidoPorId(int id)
        {
            Pedido pedidoEncontrado = _context.Pedidos.FirstOrDefault(v => v.Id == id);
            if (pedidoEncontrado != null)
            {
                RecuperaPedidoDto pedidoDto = new RecuperaPedidoDto 
                { 
                    HoraDaConsulta = DateTime.Now
                };
                return Ok(pedidoEncontrado);
            }
            return NotFound();
        }
        [HttpGet("RecuperaPedidos")]
        public IEnumerable<Pedido> RecuperaPedidos()
        {
            return _context.Pedidos;
        }
        [HttpPut("AtualizaPedido")]
        public IActionResult AtualizaPedido(int id, UpdatePedidoDto pedidoDto)
        {
            Pedido pedido = _context.Pedidos.FirstOrDefault(v => v.Id == id);
            if (pedido == null) return NotFound();

            _mapper.Map(pedidoDto, pedido);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("DeletaPedido")]
        public IActionResult DeletaPedido(int id)
        {
            Pedido pedido = _context.Pedidos.FirstOrDefault(v => v.Id == id);
            if (pedido == null) return NotFound();
            _context.Remove(pedido);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
