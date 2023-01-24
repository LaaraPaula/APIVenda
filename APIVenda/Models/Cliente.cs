using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Cliente :Pessoa
    {
        [JsonIgnore]
        public List<Pedido> Vendas { get; set; }
    }
}
