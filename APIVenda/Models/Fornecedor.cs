using System.ComponentModel.DataAnnotations;

namespace ApiVenda.Models
{
    public class Fornecedor
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereço { get; set; }

    }
}
