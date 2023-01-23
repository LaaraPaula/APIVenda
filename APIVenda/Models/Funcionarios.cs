using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIVenda.Models
{
    public class Funcionarios : Pessoa
    {
        public int Cargo { get; set; }
        [JsonIgnore]
        public virtual List<Pedido> Vendas { get; set; }
    }
}
