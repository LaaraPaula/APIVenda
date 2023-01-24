using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Fornecedor
{
    public class FornecedorDto
    {
        public int Id{ get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }
}
