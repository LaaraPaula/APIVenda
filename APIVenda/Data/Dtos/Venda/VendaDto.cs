using APIVenda.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Venda
{
    public class VendaDto
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public int ClienteId { get; set; }
        public decimal ValorCompra { get; set; }
    }
    public class ExibeVendaDto
    {
        public string ClientePedido { get; set; }
        public string FuncionarioPedido { get; set; }
        public decimal ValorCompra { get; set; }
    }
}
