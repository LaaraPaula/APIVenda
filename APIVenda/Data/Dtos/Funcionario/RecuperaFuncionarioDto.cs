using System.Collections.Generic;
using APIVenda.Models;

namespace APIVenda.Data.Dtos.Funcionario
{
    public class RecuperaFuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public object Vendas { get; set; }
    }
}
