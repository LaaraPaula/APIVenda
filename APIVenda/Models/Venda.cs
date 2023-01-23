using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Venda
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
        public virtual List<Produto> Produtos { get; set; }
        public DateTime HorarioVenda { get; set; }
    }
}
