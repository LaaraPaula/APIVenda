
using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ClienteController : ControllerBase
    {
        private DataContext _context;
        private IMapper _mapper;
        public ClienteController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AdicionarCliente")]
        public IActionResult AdicionaCliente(CreateClienteDto clienteDto)
        {
            Clientes cliente = _mapper.Map<Clientes>(clienteDto);
            if (cliente.Id == 0)
            {

                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaClientesPorId), new { Id = cliente.Id }, cliente);
        }

        [HttpGet("RecuperaClientes")]
        public IEnumerable<Clientes> RecuperaClientes()
        {
            return _context.Clientes;
        }

        [HttpGet("RecuperaClientesPorId")]
        public IActionResult RecuperaClientesPorId(int id)
        {
            Clientes clienteEncontrado = _context.Clientes.FirstOrDefault(p => p.Id == id);
            if (clienteEncontrado != null)
            {
                RecuperaClienteDto clienteDto = _mapper.Map<RecuperaClienteDto>(clienteEncontrado);
                return Ok(clienteDto);
            }
            return NotFound();
        }

        [HttpPut("AtualizaCliente")]
        public IActionResult AtualizaCliente(int id, UpdateClienteDto clienteDto)
        {
            Clientes clienteEncontrado = _context.Clientes.FirstOrDefault(p => p.Id == id);
            if (clienteEncontrado == null) return NotFound();

            _mapper.Map(clienteDto, clienteEncontrado);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("DeletaCliente")]
        public IActionResult DeletaCliente(int id)
        {
            Clientes clienteEncontrado = _context.Clientes.FirstOrDefault(p => p.Id == id);
            if (clienteEncontrado == null) return NotFound();
            _context.Remove(clienteEncontrado);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
