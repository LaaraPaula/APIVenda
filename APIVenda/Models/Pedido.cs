using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int ProdutoId { get; set; }
        public virtual Venda Venda { get; set; }
        public virtual int VendaId { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public decimal ValorUnitario { get; internal set; }
    }
}
