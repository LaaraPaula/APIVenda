using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Produto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' é obrigatório")]
        [MaxLength(60, ErrorMessage = "Nome do produto deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Nome do produto deve conter entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [MaxLength(1024, ErrorMessage = "A descrição deve conter no máximo 1024 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo 'Preco' é obrigatório")]
        [Range(1,int.MaxValue,ErrorMessage ="O preço deve ser maior que zero")]
        public decimal  PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Campo 'QuantidadeEstoque' é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int QuantidadeEstoque { get; set; }

        public virtual List<Compra> Compras { get; set; }
    }
}
