using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Pedido
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int FuncionarioId { get; set; }
        [Required]
        public virtual Funcionarios Funcionario { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public virtual Fornecedor Cliente { get; set; }
        [Required]
        public virtual Produto Produto { get; set; }
        public virtual int ProdutoId { get; set; }
        [JsonIgnore]
        public virtual Venda Venda { get; set; }
        public virtual int VendaId { get; set; }
        [Required]
        public int QuantidadeItens { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
    }
}
