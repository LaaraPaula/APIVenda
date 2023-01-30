using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Models
{
    public class Fornecedor : Pessoa
    {
        public string CNPJ { get; set; }
        public virtual List<ControladorEstoque> Estoques { get; set; }
    }
}
