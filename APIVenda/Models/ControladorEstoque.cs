using System;

namespace APIVenda.Models
{
    public class ControladorEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int FornecedorId { get; set; }
        public int QuantidadeItens { get; set; }
        public DateTime DataEntradaEstoque { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
