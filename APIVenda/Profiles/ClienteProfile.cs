using APIVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Cliente;
using System.Linq;

namespace APIVenda.Profiles
{
    public class ClienteProfile:Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteDto, Fornecedor>();
            //    CreateMap<Clientes, RecuperaClienteDto>()
            //        .ForMember(funcionario => funcionario.Vendas, opts => opts
            //        .MapFrom(Funcionarios => Funcionarios.Vendas.Select(v => new { v.Id, v.ValorCompra })));
            //    CreateMap<UpdateClienteDto, Clientes>();
        }
    }
}
