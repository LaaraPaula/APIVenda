using ApiVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Funcionario;

namespace APIVenda.Profiles
{
    public class FunfionarioProfile:Profile
    {
        public FunfionarioProfile()
        {
            CreateMap<CreateFuncionarioDto, Funcionario>();
            CreateMap<UpdateFuncionarioDto, Funcionario>();
        }
    }
}
