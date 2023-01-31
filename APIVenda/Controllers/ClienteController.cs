
using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteApp _clienteApp;

        public ClienteController(DataContext context)
        {
            _clienteApp = new ClienteApp(context);
        }

        [HttpPost("SaveClient")]
        public IActionResult SaveClient(ClienteDto clienteDto)
        {
            try
            {
                var cliente = _clienteApp.SaveClient(clienteDto);

                return Ok(cliente);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeClientes")]
        public IActionResult ExibeClientes(string nome)
        {
            try
            {
                var clientes = _clienteApp.ExibeClientes(nome);
                return Ok(clientes); 

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeClientesPorId")]
        public IActionResult ExibeClientesPorId(int id)
        {
            try
            {
                var cliente = _clienteApp.ExibePorId(id);
                return Ok(cliente);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaCliente")]
        public IActionResult DeletaCliente(int id)
        {
            try
            {
                var cliente = _clienteApp.DeletaCliente(id);
                return Ok($"CLIENTE {cliente} deletado!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
