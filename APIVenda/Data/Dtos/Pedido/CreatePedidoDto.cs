using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Pedido
{
    public class CreatePedidoDto
    {
        [Required]
        public int FuncionarioId { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int QuantidadeItens { get; set; }
    }
}
