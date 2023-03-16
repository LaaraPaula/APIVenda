using APIVenda.Data;
using APIVenda.Data.Dtos.Produto;
using Microsoft.AspNetCore.Mvc;
using APIVenda.Aplication;
using System;
using APIVenda.Data.Dtos.Estoque;
using Microsoft.Extensions.Logging;
using APIVenda.Models;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoApp _produtoApp;
        private readonly ILogger _logger;
        public ProdutoController(DataContext context, ILogger<Produto> logger)
        {
            _produtoApp= new ProdutoApp(context);
            _logger= logger;
        }

        [HttpPost("SaveProduto")]
        public IActionResult SaveProduto(ProdutoDto produtoDto)
        {
            try
            {
                var (objProduto,produto) = _produtoApp.SaveProduto(produtoDto);
                _logger.LogInformation($"{produto} produto...");
                _logger.LogInformation($"{produto} produto {objProduto.Nome} efetuado com sucesso. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(objProduto);
            }

            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao salvar produto.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AtualizaEstoque")]
        public IActionResult AtualizaEstoque(EstoqueDto estoque)
        {
            try
            {
                _logger.LogInformation("Atualizando estoque...");
                var produto = _produtoApp.AtualizaEstoque(estoque);
                _logger.LogInformation($"Estoque atualizado com sucesso. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(produto);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao atualizar estoque.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeEstoque")]
        public IActionResult ExibeEstoque(int dias)
        {
            try
            {
                _logger.LogInformation($"Buscando estoques...");
                var estoque = _produtoApp.ExibeEstoque(dias);
                _logger.LogInformation($"Lista de estoques encontrada. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(estoque);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Erro ao encontrar registro de estoques.\nErro:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeProdutos")]
        public IActionResult ExibeProdutos(string nome)
        {
            try
            {
                _logger.LogInformation($"Buscando lista de produtos...");
                var produto = _produtoApp.ExibeProdutos(nome);
                _logger.LogInformation($"Lista de produtos encontrada. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(produto);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao obter lista de produtos.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeProdutosPorId")]
        public IActionResult ExibeProdutosPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando produto por id...");
                var produto = _produtoApp.ExibePorId(id);
                _logger.LogInformation($"Produto {produto.Nome} encontrado. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(produto);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao encontrar produto.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaProduto")]
        public IActionResult DeletaProduto(int id)
        {
            try
            {
                _logger.LogInformation($"Inciando \"Deletar\"...");
                var deletado = _produtoApp.DeletaProduto(id);
                _logger.LogInformation($"Produto {deletado} com sucesso. \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok($"PRODUTO {deletado} deletado");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao deletar produto.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

    }
}
