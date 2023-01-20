
using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Data.Dtos.Funcionario;
using APIVenda.Data.Dtos.Produto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FuncionarioController : ControllerBase
    {
        private DataContext _context;
        private IMapper _mapper;

        public FuncionarioController(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("AdicionaFuncionario")]
        public IActionResult AdicionaFuncionario(CreateFuncionarioDto dto)
        {
            Funcionarios funcionario = _mapper.Map<Funcionarios>(dto);
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFuncionarioPorId), new { Id = funcionario.Id }, funcionario);
        }
        [HttpGet("RecuperaFuncionarioPorId")]
        public IActionResult RecuperaFuncionarioPorId(int id)
        {
            Funcionarios funcionario = _context.Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario != null)
            {
                RecuperaFuncionarioDto funcionarioDto = _mapper.Map<RecuperaFuncionarioDto>(funcionario);
                return Ok(funcionarioDto);
            }
            return NotFound();
        }
        [HttpGet("RecuperaFuncionarios")]
        public IEnumerable<Funcionarios> RecuperaFuncionarios()
        {
            return _context.Funcionarios;
        }
        [HttpPut("AtualizaFuncionario")]
        public IActionResult AtualizaFuncionario(int id, UpdateFuncionarioDto funcionarioDto)
        {
            Funcionarios funcionario = _context.Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario == null) return NotFound();

            _mapper.Map(funcionarioDto, funcionario);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("DeletaFuncionario")]
        public IActionResult DeletaFuncionario(int id)
        {
            Funcionarios funcionario = _context.Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario == null) return NotFound();

            _context.Remove(funcionario);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
