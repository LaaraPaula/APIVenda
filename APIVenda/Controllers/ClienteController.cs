
using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
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
        public IActionResult ExibeClientes()
        {
            try
            {
                return Ok(_clienteApp.ExibeClientes()); 

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
                _clienteApp.DeletaCliente(id);
                return Ok("CLIENTE deletado!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
