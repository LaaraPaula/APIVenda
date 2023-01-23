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
        public virtual List<Models.Venda> Compras { get; set; }
        [Required]
        public virtual APIVenda.Models.Pedido PedidoId { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
    }
}
