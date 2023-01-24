using APIVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Fornecedor;

namespace APIVenda.Profiles
{
    public class FornecedorProfile:Profile
    {
        public FornecedorProfile()
        {
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap<Fornecedor, RecuperaFornecedorDto>();
            CreateMap<UpdateFornecedorDto, Fornecedor>();
        }
    }
}
