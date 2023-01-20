using AutoMapper;
using APIVenda.Models;
using APIVenda.Data.Dtos.Venda;

namespace APIVenda.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<CreateVendaDto, Vendas>();
            CreateMap<Vendas, RecuperaVendaDto>();
            CreateMap<UpdateVendaDto, Vendas>();
        }
    }
}
