using System.Collections.Generic;

namespace APIVenda.Models
{
    public class Cliente :Pessoa
    {
        public string CPF { get; set; }
        public List<Venda> Vendas { get; set; }

    }
}
