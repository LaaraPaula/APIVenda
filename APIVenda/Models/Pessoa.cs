using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco{ get; set; }
    }
}
