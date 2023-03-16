using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Venda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController :ControllerBase
    {
        private readonly VendaApp _vendaApp;
        private readonly ILogger _logger;

        public VendaController(DataContext context,ILogger logger)
        {
            _vendaApp = new VendaApp(context);
            _logger = logger;
        }
        [HttpPost("SaveVenda")]
        public IActionResult SaveVenda(VendaDto vendaDto)
        {
            try
            {
                var (objVenda,venda) = _vendaApp.SaveVenda(vendaDto);
                _logger.LogInformation($"{venda} venda...");
                _logger.LogInformation($"{venda} venda efetuado com sucesso. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(objVenda);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao salvar venda.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeVendas")]
        public IActionResult ExibeVendas()
        {
            try
            {
                _logger.LogInformation($"Buscando lista de vendas...");
                var vendas = _vendaApp.ExibeVendas();
                _logger.LogInformation($"Lista de vendas exibida. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(vendas);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao exibir lista de vendas.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibePedidosVenda")]
        public IActionResult ExibePedidosVenda(int idVenda)
        {
            try
            {
                _logger.LogInformation($"Buscando pedidos da venda n°{idVenda}...");
                var pedidos = _vendaApp.GetPedidos(idVenda);
                _logger.LogInformation($"Lista de pedidos exibida. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao encontrar os pedidos da venda n°{idVenda}.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeVendaPorId")]
        public IActionResult ExibeVendaPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando venda por id...");
                var venda = _vendaApp.ExibePorId(id);
                _logger.LogInformation($"Venda n°{venda} encontrada. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(venda);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao encontrar venda.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaVenda")]
        public IActionResult DeletaVenda(int id)
        {
            try
            {
                _logger.LogInformation($"Iniciando \"Deletar\"...");
                var venda =_vendaApp.DeletaVenda(id);
                _logger.LogInformation($"Venda n°{venda} deletada com sucesso. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok($"VENDA {venda} deletada");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao deletar venda.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ObterPorPeriodo")]
        public IActionResult ObterPorPeriodo(int dias)
        {
            try
            {
                _logger.LogInformation($"Buscando vendas do últimos {dias} dias...");
                var vendas = _vendaApp.VendasPorPeriodo(dias);
                _logger.LogInformation($"Vendas do período exibida. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(vendas);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro encontar vendas.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
