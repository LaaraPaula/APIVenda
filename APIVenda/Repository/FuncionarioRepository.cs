﻿using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Data.Dtos.Funcionario;
using APIVenda.Data.Enum;
using APIVenda.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Repository
{
    public class FuncionarioRepository
    {
        private DataContext _context;

        public FuncionarioRepository(DataContext context)
        {
            _context = context;
        }
        public int AddFuncionario(Funcionarios funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
            return funcionario.Id;
        }

        public Funcionarios GetFuncionarioId(int id)
        {
            var funcionario = _context.Funcionarios.FirstOrDefault(funcionario => funcionario.Id == id);
            return funcionario;
        }

        public Funcionarios UpdateFuncionario(Funcionarios funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            _context.SaveChanges();
            return funcionario;
        }

        public void Excluir(Funcionarios funcionario)
        {
            _context.Remove(funcionario);
            _context.SaveChanges();
        }

        public IList<FuncionarioGetDto> GetFuncionarios()
        {
            var query = from a in _context.Funcionarios
                        select new FuncionarioGetDto
                        {
                            Nome = a.Nome,
                            Cargo = ((EnumCargo)a.Cargo).ToString(),
                            Telefone = a.Telefone
                        };
            return query.ToList();;
        }

    }
}
