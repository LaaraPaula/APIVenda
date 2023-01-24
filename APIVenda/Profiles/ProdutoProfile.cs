using APIVenda.Models;
using APIVenda.Data.Dtos.Produto;
using AutoMapper;

namespace APIVenda.Profiles
{
    public class ProdutoProfile  : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoDto, Produto>();
            CreateMap<Produto, RecuperaProdutoDto>();
            CreateMap<UpdateProdutoDto, Produto>();
        }
    }
}
