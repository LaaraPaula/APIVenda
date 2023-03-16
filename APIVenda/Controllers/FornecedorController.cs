using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Fornecedor;
using APIVenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FornecedorController :ControllerBase
    {
        private FornecedorApp _fornecedorApp;
        private readonly ILogger _logger;
        public FornecedorController(DataContext context, ILogger<Fornecedor> logger)
        {
            _fornecedorApp = new FornecedorApp(context);
            _logger = logger;
        }

        [HttpPost("SaveFornecedor")]
        public IActionResult SaveFornecedor(FornecedorDto fornecedorDto)
        {
            try
            {
                var (objRetorno, fornecedor) = _fornecedorApp.SaveFornecedor(fornecedorDto);
                _logger.LogInformation($"{fornecedor} fornecedor...");
                _logger.LogInformation($"{fornecedor} fornecedor {objRetorno.Nome} efetuado com sucesso");
                return Ok(objRetorno);
            }

            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao salvar fornecedor.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeFornecedores")]
        public IActionResult ExibeFornecedores(string nome)
        {
            try
            {
                _logger.LogInformation("Buscando lista de fornecedores...");
                var fornecedor = _fornecedorApp.ExibeFornecedores(nome);
                _logger.LogInformation($"Lista de fornecedores exbida \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(fornecedor);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao exibir lista de fornecedores.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeFornecedorPorId")]
        public IActionResult ExibeFornecedorPorId(int id)
        {
            try
            {
                _logger.LogInformation("Buscando Fornecedor por id...");
                var fornecedor = _fornecedorApp.ExibePorId(id);
                _logger.LogInformation($"Fornecedor {fornecedor.Nome} encontrado \t {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                return Ok(fornecedor);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao obter fornecedor.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaFornecedor")]
        public IActionResult DeletaFornecedor(int id)
        {
            try
            {
                _logger.LogInformation("Iniciando \"Deletar\"...");
                var fornecedor = _fornecedorApp.DeletaFornecedor(id);
                _logger.LogInformation($"Fornecedor {fornecedor} deletado \t {DateTime.Now:dd/MM/yyy HH:mm:ss}");
                return Ok($"FORNECEDOR {fornecedor} deletado");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao deletar fornecedor.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
    }
}
