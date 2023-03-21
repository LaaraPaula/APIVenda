using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoApp _pedidoApp;
        private readonly ILogger _logger;
        public PedidoController(DataContext context , ILogger<Pedido> logger)
        {
            _pedidoApp = new PedidoApp(context);
            _logger= logger;
        }
        [HttpPost("SavePedido")]
        public IActionResult SavePedido(PedidoDto pedidoDto)
        {
            try
            {
                var (objPedido,pedido) = _pedidoApp.SavePedido(pedidoDto);
                _logger.LogInformation($"{pedido} pedido...");
                _logger.LogInformation($"{pedido} pedido n°{objPedido.Id} efetuado com sucesso.");
                return Ok(objPedido);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao salvar pedido.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibePedidos")]
        public IActionResult ExibePedidos()
        {
            try
            {
                _logger.LogInformation("Buscando lista de pedidos...");
                var pedidos = _pedidoApp.ExibePedidos();
                _logger.LogInformation("Lista de pedidos exibida com sucesso.");
                return Ok(pedidos);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao exibir lista de pedido.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibePedidoPorId")]
        public IActionResult ExibePedidoPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando pedido por id...");
                var pedido = _pedidoApp.ExibePorId(id);
                _logger.LogInformation($"Pedido n°{pedido.Id} encontrado.");
                return Ok(pedido);

            }
            catch (Exception ex)
            { 
                _logger.LogInformation($"Erro ao encontrar pedido.\nErro:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaPedido")]
        public IActionResult DeletaPedido(int id)
        {
            try
            {
                _logger.LogInformation("Iniciando \"Deletar\"...");
                var pedido = _pedidoApp.DeletaPedido(id);
                _logger.LogInformation($"Pedido n°{pedido} deletado com sucesso.");
                return Ok($"PEDIDO {pedido} deletado");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao deletar pedido.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
