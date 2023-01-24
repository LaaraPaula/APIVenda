
using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Data.Dtos.Funcionario;
using APIVenda.Data.Dtos.Produto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using APIVenda.Aplication;
using APIVenda.Data.Dtos.Cliente;
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
        [HttpPost("SaveFuncionario")]
        public IActionResult SaveFuncionario(FuncionarioDto funcionarioDto)
        {
            try
            {
                var funcionario = _funcionarioApp.SaveClient(funcionarioDto);

                return Ok(funcionario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeFuncionarios")]
        public IActionResult ExibeFuncionarios()
        {
            try
            {
                return Ok(_funcionarioApp.ExibeFuncionarios());

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
                _funcionarioApp.DeletaFuncionario(id);
                return Ok("FUNCIONARIO deletado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ObterCargos")]
        public IActionResult ObterCargos()
        {
            return Ok(_funcionarioApp.GetCargos());
        }
    }
}
