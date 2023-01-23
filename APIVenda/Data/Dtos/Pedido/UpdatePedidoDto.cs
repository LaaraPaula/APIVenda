using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Pedido
{
    public class UpdatePedidoDto
    {
        [Required]
        public int FuncionarioId { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public virtual int ProdutoId { get; set; }
        [Required]
        public virtual int VendaId { get; set; }
        [Required]
        public int QuantidadeItens { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
    }
}
