using System;

namespace APIVenda.Data.Dtos.Estoque
{
    public class EstoqueDto
    {
        public int ProdutoId { get; set; }
        public int FornecedorId { get; set; }
        public int QuantidadeItens { get; set; }
    }
    public class ExibeEstoqueDto
    {
        public string Produto { get; set; }
        public string FornecedorProduto { get; set; }
        public int QuantidadeItens { get; set; }
        public DateTime DataEntradaEstoque { get; set; }
        public int TotalEstoque { get; set; }
    }
}
