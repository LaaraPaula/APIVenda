using System;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Compra
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual Vendas Venda { get; set; }
        public virtual Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public int VendaId { get; set; }
        public DateTime HorarioVenda { get; set; }
    }
}
