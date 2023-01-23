using APIVenda.Data;
using APIVenda.Data.Dtos.Fornecedor;
using APIVenda.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FornecedorController :ControllerBase
    {
        private DataContext _context;
        private IMapper _mapper;
        public FornecedorController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AdicionarFornecedor")]
        public IActionResult AdicionaFornecedor(CreateFornecedorDto fornecedorDto)
        {
            Fornecedor fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
            if (fornecedor.Id == 0)
            {
                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();
            }
            return CreatedAtAction(nameof(RecuperarFornecedorPorId), new { Id = fornecedor.Id }, fornecedor);
        }

        [HttpGet("RecuperarFornecedor")]
        public IEnumerable<Fornecedor> RecuperarFornecedor()
        {
            return _context.Fornecedores;
        }

        [HttpGet("RecuperarFornecedorPorId")]
        public IActionResult RecuperarFornecedorPorId(int id)
        {
            Fornecedor fornecedorEncontrado = _context.Fornecedores.FirstOrDefault(p => p.Id == id);
            if (fornecedorEncontrado != null)
            {
                RecuperaFornecedorDto fornecedorDto = _mapper.Map<RecuperaFornecedorDto>(fornecedorEncontrado);
                return Ok(fornecedorDto);
            }
            return NotFound();
        }

        [HttpPut("AtualizaFornecedor")]
        public IActionResult AtualizaFornecedor(int id, UpdateFornecedorDto fornecedorDto)
        {
            Fornecedor fornecedorEncontrado = _context.Fornecedores.FirstOrDefault(p => p.Id == id);
            if (fornecedorEncontrado == null) return NotFound();

            _mapper.Map(fornecedorDto, fornecedorEncontrado);
            _context.SaveChanges();
            return Ok(fornecedorDto);

        }

        [HttpDelete("DeletaFornecedor")]
        public IActionResult DeletaFornecedor(int id)
        {
            Fornecedor fornecedorEncontrado = _context.Fornecedores.FirstOrDefault(p => p.Id == id);
            if (fornecedorEncontrado == null) return NotFound();
            _context.Remove(fornecedorEncontrado);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
