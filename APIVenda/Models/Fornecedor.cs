using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Fornecedor : Pessoa
    {
        public string CNPJ { get; set; }
    }
}
