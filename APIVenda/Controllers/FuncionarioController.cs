using APIVenda.Data;
using APIVenda.Data.Dtos.Funcionario;
using Microsoft.AspNetCore.Mvc;
using APIVenda.Aplication;
using System;
using Microsoft.Extensions.Logging;
using APIVenda.Models;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioApp _funcionarioApp;
        private readonly ILogger _logger;

        public FuncionarioController(DataContext context,ILogger<Funcionarios> logger)
        {
            _funcionarioApp = new FuncionarioApp(context);
            _logger= logger;
        }
        [HttpGet("ObterCargos")]
        public IActionResult ObterCargos(string nome)
        {
            try
            {
                _logger.LogInformation("Obtendo cargos...");
                return Ok(_funcionarioApp.GetCargos());
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao obter cargos.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveFuncionario")]
        public IActionResult SaveFuncionario(FuncionarioDto funcionarioDto)
        {
            try
            {
                var (objFuncionario,funcionario) = _funcionarioApp.SaveFuncionario(funcionarioDto);
                _logger.LogInformation($"{funcionario} funcionario(a)...");
                _logger.LogInformation($"{funcionario} do(a) funcionário(a) {objFuncionario.Nome} efetuado com sucesso.");
                return Ok(objFuncionario);
            }

            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao salvar funcionário.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeFuncionarios")]
        public IActionResult ExibeFuncionarios(string nome)
        {
            try
            {
                _logger.LogInformation("Buscando lista de funcionários...");
                var funcionario = _funcionarioApp.ExibeFuncionarios(nome);
                _logger.LogInformation($"Lista de funcionários exibidos com sucesso");
                return Ok(funcionario);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao exibir lista de funcionarios.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeFuncionarioPorId")]
        public IActionResult ExibeFuncionarioPorId(int id)
        {
            try
            {
                _logger.LogInformation("Buscando funcionário por id...");
                var funcionario = _funcionarioApp.ExibePorId(id);
                _logger.LogInformation($"Funcionário {funcionario.Nome} encontrado.");
                return Ok(funcionario);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao exibir funcionário.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaFuncionario")]
        public IActionResult DeletaFuncionario(int id)
        {
            try
            {
                _logger.LogInformation("Iniciando \"Deletar\'...");
                var funcionario = _funcionarioApp.DeletaFuncionario(id);
                _logger.LogInformation($"Funcionario {funcionario} deletado com sucesso.");
                return Ok($"FUNCIONARIO {funcionario} deletado");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro ao deletar funcionário.\nErro: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
        
    }
}
