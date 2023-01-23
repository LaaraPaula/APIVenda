using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController :ControllerBase
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
            Venda venda = _mapper.Map<Venda>(dto);
            _context.Vendas.Add(venda);
            foreach (var item in venda.Pedidos)
            {
                if (item.Id == 0)
                {
                    item.VendaId = venda.Id;
                    _context.Pedidos.Add(item);
                }
            }
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaVendaPorId), new { Id = venda.Id},venda);
        }

        private object RecuperaVendaPorId(int id)
        {
            Venda vendaEncontrado = _context.Vendas.FirstOrDefault(v => v.Id == id);
            if (vendaEncontrado != null)
            {
                RecuperaVendaDto vendaDto = _mapper.Map<RecuperaVendaDto>(vendaEncontrado);

                vendaDto.HoraDaConsulta = DateTime.Now;
                
                return Ok(vendaDto);
            }
            return NotFound();
        }
    }
}
