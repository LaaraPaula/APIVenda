using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Fornecedor;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FornecedorController :ControllerBase
    {
        private FornecedorApp _fornecedorApp;
        public FornecedorController(DataContext context)
        {
            _fornecedorApp = new FornecedorApp(context);
        }

        [HttpPost("SaveFornecedor")]
        public IActionResult SaveFornecedor(FornecedorDto fornecedorDto)
        {
            try
            {
                var fornecedor = _fornecedorApp.SaveFornecedor(fornecedorDto);

                return Ok(fornecedor);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeFornecedores")]
        public IActionResult ExibeFornecedores(string nome)
        {
            try
            {
                var fornecedor = _fornecedorApp.ExibeFornecedores(nome);
                return Ok(fornecedor);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeFornecedorPorId")]
        public IActionResult ExibeFornecedorPorId(int id)
        {
            try
            {
                var fornecedor = _fornecedorApp.ExibePorId(id);
                return Ok(fornecedor);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaFornecedor")]
        public IActionResult DeletaFornecedor(int id)
        {
            try
            {
                var fornecedor = _fornecedorApp.DeletaFornecedor(id);
                return Ok($"FORNECEDOR {fornecedor} deletado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
