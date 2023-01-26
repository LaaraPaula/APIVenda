using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Cliente :Pessoa
    {
        public List<Venda> Vendas { get; set; }

    }
}
