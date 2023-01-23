using APIVenda.Data.Dtos.Pedido;
using APIVenda.Data.Dtos.Venda;
using APIVenda.Models;
using AutoMapper;

namespace APIVenda.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {

            CreateMap<CreatePedidoDto, Pedido>();
            CreateMap<Pedido, RecuperaPedidoDto>();
            CreateMap<UpdatePedidoDto, Pedido>();
        }
    }
}
