using ApiVenda.Models;
using AutoMapper;
using APIVenda.Data.Dtos.Cliente;

namespace APIVenda.Profiles
{
    public class ClienteProfile:Profile
    {
        public ClienteProfile()
        {
            CreateMap<CreateClienteDto, Cliente>();
            CreateMap<UpdateClienteDto, Cliente>();
        }
    }
}
