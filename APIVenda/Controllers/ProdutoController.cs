using APIVenda.Data;
using APIVenda.Data.Dtos.Produto;
using Microsoft.AspNetCore.Mvc;
using APIVenda.Aplication;
using System;
using APIVenda.Data.Dtos.Estoque;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoApp _produtoApp;
        public ProdutoController(DataContext context)
        {
            _produtoApp= new ProdutoApp(context);
        }

        [HttpPost("SaveProduto")]
        public IActionResult SaveProduto(ProdutoDto produtoDto)
        {
            try
            {
                var produto = _produtoApp.SaveProduto(produtoDto);

                return Ok(produto);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AtualizaEstoque")]
        public IActionResult AtualizaEstoque(EstoqueDto estoque)
        {
            try
            {
                var produto = _produtoApp.AtualizaEstoque(estoque);
                return Ok(produto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeProdutos")]
        public IActionResult ExibeProdutos(string nome)
        {
            try
            {
                var produto = _produtoApp.ExibeProdutos(nome);
                return Ok(produto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeProdutosPorId")]
        public IActionResult ExibeProdutosPorId(int id)
        {
            try
            {
                var produto = _produtoApp.ExibePorId(id);
                return Ok(produto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaProduto")]
        public IActionResult DeletaProduto(int id)
        {
            try
            {
                var deletado = _produtoApp.DeletaProduto(id);
                return Ok($"PRODUTO {deletado} deletado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
