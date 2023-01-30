using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataVenda { get; set; }
        public virtual Funcionarios Funcionario { get; set; }
        public virtual int FuncionarioId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual int ClienteId { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
    }
}
