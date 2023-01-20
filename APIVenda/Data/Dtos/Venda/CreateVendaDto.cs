using APIVenda.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Venda
{
    public class CreateVendaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int FuncionarioId { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public virtual List<Compra> Compras { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
    }
}
