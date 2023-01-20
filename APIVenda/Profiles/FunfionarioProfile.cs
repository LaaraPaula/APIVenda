using APIVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Funcionario;
using System.Linq;

namespace APIVenda.Profiles
{
    public class FunfionarioProfile:Profile
    {
        public FunfionarioProfile()
        {
            CreateMap<CreateFuncionarioDto, Funcionarios>();
            CreateMap<Funcionarios, RecuperaFuncionarioDto>()
                .ForMember(funcionario=> funcionario.Vendas, opts=>opts
                .MapFrom(Funcionarios=> Funcionarios.Vendas.Select(v=> new {v.Id,v.ValorCompra})));
            CreateMap<UpdateFuncionarioDto, Funcionarios>();

        }
    }
}
