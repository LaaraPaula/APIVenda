
using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteApp _clienteApp;
        private readonly ILogger _logger;


        public ClienteController(DataContext context, ILogger<Cliente> logger)
        {
            _clienteApp = new ClienteApp(context);
            _logger = logger;
        }

        [HttpPost("SaveClient")]
        public IActionResult SaveClient(ClienteDto clienteDto)
        {
            try
            {                
                var (objCliente,cliente) = _clienteApp.SaveClient(clienteDto);
                _logger.LogInformation($"{cliente} cliente...");
                _logger.LogInformation($"{cliente} cliente {objCliente.Nome} feito com sucesso \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(objCliente);
            }

            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao salvar cliente.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeClientes")]
        public IActionResult ExibeClientes(string nome)
        {
            try
            {
                _logger.LogInformation("Buscando lista de clientes...");
                var clientes = _clienteApp.ExibeClientes(nome);
                _logger.LogInformation($"Lista de cliente exbida \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(clientes); 

            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao exibir lista de clientes.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeClientesPorId")]
        public IActionResult ExibeClientesPorId(int id)
        {
            try
            {
                _logger.LogInformation("Buscando cliente por id...");
                var cliente = _clienteApp.ExibePorId(id);
                _logger.LogInformation($"Cliente {cliente.Nome} encontrado \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(cliente);

            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao encontrar cliente.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaCliente")]
        public IActionResult DeletaCliente(int id)
        {
            try
            {
                _logger.LogInformation("Iniciando \"Deletar\"...");
                var cliente = _clienteApp.DeletaCliente(id);
                _logger.LogInformation($"Cliente {cliente} deletado \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

                return Ok($"CLIENTE {cliente} deletado");
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao deletar cliente.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
