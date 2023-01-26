using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal  PrecoUnitario { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int PedidoId { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
    }
}
