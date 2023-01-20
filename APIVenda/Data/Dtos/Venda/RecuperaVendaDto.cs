using ApiVenda.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Venda
{
    public class RecuperaVendaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int FuncionarioId { get; set; }

        //public Funcionario Funcionario { get; set; }
        public int ClienteId { get; set; }
        //public Cliente Cliente { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
        public DateTime HoraDaConsulta { get; set; }
    }
}
