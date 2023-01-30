using System.Collections.Generic;

namespace APIVenda.Models
{
    public class Funcionarios : Pessoa
    {
        public int Cargo { get; set; }
        public string CPF { get; set; }
        public virtual List<Venda> Vendas { get; set; }
    }
}
