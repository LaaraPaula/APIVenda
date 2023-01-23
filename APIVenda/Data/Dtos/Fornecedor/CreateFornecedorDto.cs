using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Fornecedor
{
    public class CreateFornecedorDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereco { get; set; }
    }
}
