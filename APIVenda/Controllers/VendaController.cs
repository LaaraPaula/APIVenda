﻿using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APIVenda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController :ControllerBase
    {
        private readonly VendaApp _vendaApp;

        public VendaController(DataContext context)
        {
            _vendaApp = new VendaApp(context);
        }
        [HttpPost("SaveVenda")]
        public IActionResult SaveVenda(VendaDto vendaDto)
        {
            try
            {
                var venda = _vendaApp.SaveVenda(vendaDto);
                return Ok(venda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ExibeVendas")]
        public IActionResult ExibeVendas()
        {
            try
            {
                return Ok(_vendaApp.ExibeVendas());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExibeVendaPorId")]
        public IActionResult ExibeVendaPorId(int id)
        {
            try
            {
                var pedido = _vendaApp.ExibePorId(id);
                return Ok(pedido);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletaVenda")]
        public IActionResult DeletaVenda(int id)
        {
            try
            {
                _vendaApp.DeletaVenda(id);
                return Ok("VENDA deletada!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("ObterPorPeriodo")]
        public IActionResult ObterPorPeriodo(int dias)
        {
            try
            {
                var vendas = _vendaApp.VendasPorPeriodo(dias);
                return Ok(vendas);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
