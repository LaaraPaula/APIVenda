using APIVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Fornecedor;

namespace APIVenda.Profiles
{
    public class FornecedorProfile:Profile
    {
        public FornecedorProfile()
        {
            CreateMap<CreateFornecedorDto, Fornecedor>();
            CreateMap<Fornecedor, RecuperaFornecedorDto>();
            CreateMap<UpdateFornecedorDto, Fornecedor>();
        }
    }
}
