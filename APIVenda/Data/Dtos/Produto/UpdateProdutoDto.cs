using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Produto
{
    public class UpdateProdutoDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal PrecoUnitario { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
    }
}
