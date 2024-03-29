﻿using APIVenda.Aplication;
using APIVenda.Data;
using APIVenda.Data.Dtos.Fornecedor;
using APIVenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Repository
{
    public class FornecedorRepository
    {
        private DataContext _context;

        public FornecedorRepository(DataContext context)
        {
            _context = context;
        }

        public int AddFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();
            return fornecedor.Id;
        }

        public Fornecedor GetFornecedorId(int id)
        {
            var fornecedor = _context.Fornecedores.FirstOrDefault(fornecedor=> fornecedor.Id == id);
            return fornecedor;
        }

        public Fornecedor UpdateFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
            _context.SaveChanges();
            return fornecedor;
        }

        public void Excluir(Fornecedor cliente)
        {
            _context.Remove(cliente);
            _context.SaveChanges();
        }

        public IList<FornecedorDto> GetFornecedores(string nome)
        {
            var query = from a in _context.Fornecedores
                        select new FornecedorDto
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Telefone = a.Telefone,
                            Endereco = a.Endereco,
                            CNPJ=a.CNPJ
                        };
            if (!string.IsNullOrEmpty(nome)) query = query.Where(x => x.Nome.Contains(nome));
            return query.ToList();
        }

        public Fornecedor ObtemCNPJ(string cnpj)
        {
            var fornecedorCpf = _context.Fornecedores.FirstOrDefault(x => x.CNPJ == cnpj);
            return fornecedorCpf;
        }
    }
}
