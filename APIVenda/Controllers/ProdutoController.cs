using ApiVenda.Models;
using APIVenda.Data;
using APIVenda.Data.Dtos.Produto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoContext _context;
        private IMapper _mapper;
        public ProdutoController(ProdutoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AdicionarProduto")]
        public IActionResult AdicionaProduto(CreateProdutoDto produtoDto)
        {
            Produto produto = _mapper.Map<Produto>(produtoDto);
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaProdutosPorId), new { Id = produto.Id }, produto);
        }

        [HttpGet("RecuperaProdutos")]
        public IEnumerable<Produto> RecuperaProdutos()
        {
            return _context.Produtos;
        }

        [HttpGet("RecuperaProdutosPorId")]
        public IActionResult RecuperaProdutosPorId(int id)
        {
            Produto produtoEncontrado = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produtoEncontrado != null) return Ok(produtoEncontrado);
            return NotFound();
        }

        [HttpPut("AtualizaProduto")]
        public IActionResult AtualizaProduto(int id , UpdateProdutoDto produtoDto)
        {
            Produto produtoEncontrado = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produtoEncontrado == null) return NotFound();

            _mapper.Map(produtoDto,produtoEncontrado);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("DeletaProduto")]
        public IActionResult DeletaProduto(int id)
        {
            Produto produtoEncontrado = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produtoEncontrado == null) return NotFound();
            _context.Remove(produtoEncontrado);
            _context.SaveChanges();
            return NoContent();

        }
        
    }
}
