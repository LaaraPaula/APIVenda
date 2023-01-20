using ApiVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Fornecedor;

namespace APIVenda.Profiles
{
    public class FornecedorProfile:Profile
    {
        public FornecedorProfile()
        {
            CreateMap<CreateFornecedorDto, Fornecedor>();
            CreateMap<UpdateFornecedorDto, Fornecedor>();
        }
    }
}
