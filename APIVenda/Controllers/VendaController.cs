using ApiVenda.Models;
using APIVenda.Data;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
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
        private VendaContext _context;
        public VendaController(VendaContext context)
        {
            _context = context;
        }

        //[HttpGet("{id}")]
        //public IActionResult RecuperaVendaPorId(int id)
        //{
        //    Venda vendaEncontrado = _context.Vendas.FirstOrDefault(v => v.Id == id);
        //    if (vendaEncontrado != null)
        //    {
        //        RecuperaVendaDto vendaDto = new RecuperaVendaDto 
        //        { 
        //            HoraDaConsulta = DateTime.Now
        //        };
        //        return Ok(vendaEncontrado);
        //    }
        //    return NotFound();
        //}
    }
}
