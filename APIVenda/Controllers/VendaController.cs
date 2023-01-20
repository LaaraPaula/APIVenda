using APIVenda.Data;
using APIVenda.Data.Dtos.Funcionario;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class VendaController : ControllerBase
    {
        private DataContext _context;
        private IMapper _mapper;
        public VendaController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("AdicionaVenda")]
        public IActionResult AdicionaVenda(CreateVendaDto dto)
        {
            Vendas venda = _mapper.Map<Vendas>(dto);
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaVendaPorId), new { Id = venda.Id }, venda);
        }
        [HttpGet("RecuperaVendaPorId")]
        public IActionResult RecuperaVendaPorId(int id)
        {
            Vendas vendaEncontrado = _context.Vendas.FirstOrDefault(v => v.Id == id);
            if (vendaEncontrado != null)
            {
                RecuperaVendaDto vendaDto = new RecuperaVendaDto 
                { 
                    HoraDaConsulta = DateTime.Now
                };
                return Ok(vendaEncontrado);
            }
            return NotFound();
        }
        public IEnumerable<Vendas> RecuperaVenda()
        {
            return _context.Vendas;
        }
        [HttpPut("AtualizaVenda")]
        public IActionResult AtualizaVenda(int id, UpdateVendaDto vendaDto)
        {
            Vendas venda = _context.Vendas.FirstOrDefault(v => v.Id == id);
            if (venda == null) return NotFound();

            _mapper.Map(vendaDto, venda);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("DeletaVenda")]
        public IActionResult DeletaVenda(int id)
        {
            Vendas venda = _context.Vendas.FirstOrDefault(v => v.Id == id);
            if (venda == null) return NotFound();
            _context.Remove(venda);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
