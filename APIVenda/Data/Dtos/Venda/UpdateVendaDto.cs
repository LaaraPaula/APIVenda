using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Venda
{
    public class UpdateVendaDto
    {
        [Required]
        public virtual APIVenda.Models.Pedido PedidoId { get; set; }
    }
}
