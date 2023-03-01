using APIVenda.Data;
using APIVenda.Data.Dtos.Funcionario;
using Microsoft.AspNetCore.Mvc;
using APIVenda.Aplication;
using System;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioApp _funcionarioApp;

        public FuncionarioController(DataContext context)
        {
            _funcionarioApp = new FuncionarioApp(context);
        }
        [HttpGet("ObterCargos")]
        public IActionResult ObterCargos(string nome)
        {
            try
            {
                return Ok(_funcionarioApp.GetCargos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveFuncionario")]
        public IActionResult SaveFuncionario(FuncionarioDto funcionarioDto)
        {
            try
            {
                var funcionario = _funcionarioApp.SaveFuncionario(funcionarioDto);

                return Ok(funcionario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeFuncionarios")]
        public IActionResult ExibeFuncionarios(string nome)
        {
            try
            {
                var funcionario = _funcionarioApp.ExibeFuncionarios(nome);
                return Ok(funcionario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeFuncionarioPorId")]
        public IActionResult ExibeFuncionarioPorId(int id)
        {
            try
            {
                var funcionario = _funcionarioApp.ExibePorId(id);
                return Ok(funcionario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaFuncionario")]
        public IActionResult DeletaFuncionario(int id)
        {
            try
            {
                var funcionario = _funcionarioApp.DeletaFuncionario(id);
                return Ok($"FUNCIONARIO {funcionario} deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
    }
}
