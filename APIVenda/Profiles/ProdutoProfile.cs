using ApiVenda.Models;
using APIVenda.Data.Dtos.Produto;
using AutoMapper;

namespace APIVenda.Profiles
{
    public class ProdutoProfile  : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap<UpdateProdutoDto, Produto>();
        }
    }
}
