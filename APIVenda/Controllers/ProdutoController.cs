using APIVenda.Models;
using APIVenda.Data;
using APIVenda.Data.Dtos.Produto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using APIVenda.Aplication;
using APIVenda.Data.Dtos.Cliente;
using System;

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
        [HttpGet("ExibeProdutos")]
        public IActionResult ExibeProdutos()
        {
            try
            {
                return Ok(_produtoApp.ExibeProdutos());

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
                _produtoApp.DeletaProduto(id);
                return Ok("PRODUTO deletado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
