﻿using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Funcionario;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using APIVenda.Data.Enum;
using System.Text.RegularExpressions;
using APIVenda.Data.Dtos.Cliente;

namespace APIVenda.Aplication
{
    public class FuncionarioApp
    {
        private readonly FuncionarioRepository _funcionarioRepository;

        public FuncionarioApp(DataContext context)
        {
            _funcionarioRepository = new FuncionarioRepository(context);
        }

        public FuncionarioDto SaveFuncionario(FuncionarioDto funcionarioDto)
        {
            if (string.IsNullOrEmpty(funcionarioDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(funcionarioDto.Telefone)) throw new Exception("Necessário preencher o campo telefone");
            if (!Regex.IsMatch(funcionarioDto.Telefone, @"^\d{6,7}[-]?\d{4}$")) throw new Exception("Telefone em formato inválido\nEX:1199999-9999");
            if (string.IsNullOrEmpty(funcionarioDto.Endereco)) throw new Exception("Necessário preencher o campo endereço");
            if (funcionarioDto.Cargo < 0) throw new Exception("Necessário preencher o campo cargo");

            Funcionarios funcionario;
            if (funcionarioDto.Id == 0)
            {

                funcionario = new Funcionarios
                {
                    Nome = funcionarioDto.Nome,
                    Telefone = funcionarioDto.Telefone,
                    Endereco = funcionarioDto.Endereco,
                    Cargo = (int)funcionarioDto.Cargo
                };

                funcionarioDto.Id = _funcionarioRepository.AddFuncionario(funcionario);
            }
            else
            {
                funcionario = _funcionarioRepository.GetFuncionarioId(funcionarioDto.Id) ?? throw new Exception("Funcionario não encontrado");

                funcionario.Nome = funcionarioDto.Nome;
                funcionario.Endereco = funcionarioDto.Endereco;
                funcionario.Telefone = funcionarioDto.Telefone;
                funcionario.Cargo = (int)funcionarioDto.Cargo;

                _funcionarioRepository.UpdateFuncionario(funcionario);
            }

            return funcionarioDto;
        }

        public void DeletaFuncionario(int id)
        {
            var funcionario = _funcionarioRepository.GetFuncionarioId(id) ?? throw new Exception("Funcionario não encontrado");
            _funcionarioRepository.Excluir(funcionario);
        }

        public IList<FuncionarioGetDto> ExibeFuncionarios()
        {
            var funcionarios = _funcionarioRepository.GetFuncionarios();
            return funcionarios;
        }

        public FuncionarioDto ExibePorId(int id)
        {
            var funcionario = _funcionarioRepository.GetFuncionarioId(id) ?? throw new Exception("Funcionario não encontrado");

            return new FuncionarioDto
            {
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone
            };
        }

        public List<EnumCargoAtributo> GetCargos()
        {
            var cargos = EnumCargoModel.GetAtributo();

            return cargos;

            //var exibe = cargos.Concat(idCargo);
            //return String.Join(",", exibe);


            //var list = new List<string>();
            //
            //for (int i = 0; i < cargos.Length; i++)
            //{
            //    list.Add($"{i + 1} - {cargos[i]}");
            //}
            //
            //return list;
        }
    }
}