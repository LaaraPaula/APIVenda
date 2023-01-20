using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Pessoa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereco{ get; set; }
    }
}
