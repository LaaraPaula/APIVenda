using System.Collections.Generic;

namespace APIVenda.Models
{
    public class Fornecedor : Pessoa
    {
        public string CNPJ { get; set; }
        public virtual List<ControladorEstoque> Estoques { get; set; }
    }
}
