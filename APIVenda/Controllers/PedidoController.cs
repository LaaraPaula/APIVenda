using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoApp _pedidoApp;
        public PedidoController(DataContext context)
        {
            _pedidoApp = new PedidoApp(context);
        }
        [HttpPost("SavePedido")]
        public IActionResult SavePedido(PedidoDto pedidoDto)
        {
            try
            {
                var pedido = _pedidoApp.SavePedido(pedidoDto);
                return Ok(pedido);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibePedidos")]
        public IActionResult ExibePedidos()
        {
            try
            {
                return Ok(_pedidoApp.ExibePedidos());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibePedidoPorId")]
        public IActionResult ExibePedidoPorId(int id)
        {
            try
            {
                var pedido = _pedidoApp.ExibePorId(id);
                return Ok(pedido);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaPedido")]
        public IActionResult DeletaPedido(int id)
        {
            try
            {
                var pedido = _pedidoApp.DeletaPedido(id);
                return Ok($"PEDIDO {pedido} deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
