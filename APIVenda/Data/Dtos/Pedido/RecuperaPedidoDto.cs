using APIVenda.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Pedido
{
    public class RecuperaPedidoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
        public DateTime HoraDaConsulta { get; set; }
    }
}
