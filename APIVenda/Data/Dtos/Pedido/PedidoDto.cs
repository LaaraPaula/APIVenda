namespace APIVenda.Data.Dtos.Pedido
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int QuantidadeItens { get; set; }
        
    }
    public class ExibePedidoDto
    {
        public int Id { get; set; }
        public string ProdutoPedido { get; set; }
        public int QuantidadeProduto { get; internal set; }
        public decimal ValorUnidade { get; internal set; }
        public decimal ValorPedido { get; internal set; }
    }


}
