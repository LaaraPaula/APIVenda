using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Repository
{
    public class ClienteRepository
    {
        private DataContext _context;

        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public int AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente.Id;
        }

        public Cliente GetClienteId(int id)
        {
            return _context.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public Cliente UpdateCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public void Excluir(Cliente cliente)
        {
            _context.Remove(cliente);
            _context.SaveChanges();
        }

        public IList<ClienteDto> GetClientes(string nome)
        {
            var query = from a in _context.Clientes
                        select new ClienteDto
                        {
                            Id=a.Id,
                            CPF=a.CPF,
                            Nome = a.Nome,
                            Telefone = a.Telefone,
                            Endereco = a.Endereco
                        };
            if (!string.IsNullOrEmpty(nome)) query = query.Where(x => x.Nome.Contains(nome));
            return query.ToList();
        }

        public Cliente ObtemCPF(string cpf)
        {
            var clienteCpf = _context.Clientes.FirstOrDefault(x => x.CPF == cpf);
            return clienteCpf;
        }
    }
}
