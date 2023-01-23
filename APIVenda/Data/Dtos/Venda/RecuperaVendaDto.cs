﻿using APIVenda.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Venda
{
    public class RecuperaVendaDto
    {
        public int Id { get; set; }
        public Funcionarios Funcionario { get; set; }
        public Clientes Cliente { get; set; }
        public APIVenda.Models.Produto Produtos { get; set; }
        public APIVenda.Models.Pedido Pedido  { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime HoraDaConsulta { get; set; }
    }
}
