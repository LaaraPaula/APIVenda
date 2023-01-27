using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Cliente :Pessoa
    {
        public string CPF { get; set; }
        public List<Venda> Vendas { get; set; }

    }
}
