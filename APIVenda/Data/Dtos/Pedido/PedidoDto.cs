﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Pedido
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int ValorUnitario { get; set; }
        public int QuantidadeItens { get; set; }
        public int ValorTotalPedido { get; set; }
        
    }
    public class ExibePedidoDto
    {
        public string ProdutoPedido { get; set; }
        public int QuantidadeProduto { get; internal set; }
        public decimal ValorUnidade { get; internal set; }
        public decimal ValorPedido { get; internal set; }
    }


}