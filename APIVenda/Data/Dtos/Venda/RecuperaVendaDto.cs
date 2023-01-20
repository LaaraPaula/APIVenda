﻿using APIVenda.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Venda
{
    public class RecuperaVendaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public Funcionarios Funcionario { get; set; }
        public Clientes Cliente { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
        public DateTime HoraDaConsulta { get; set; }
    }
}
