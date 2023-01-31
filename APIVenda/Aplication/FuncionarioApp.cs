using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Funcionario;
using System.Collections.Generic;
using System.Linq;
using APIVenda.Data.Enum;
using System.Text.RegularExpressions;

namespace APIVenda.Aplication
{
    public class FuncionarioApp
    {
        private readonly FuncionarioRepository _funcionarioRepository;
        private readonly VendaRepository _vendaRepository;

        public FuncionarioApp(DataContext context)
        {
            _funcionarioRepository = new FuncionarioRepository(context);
            _vendaRepository = new VendaRepository(context);
        }

        public FuncionarioDto SaveFuncionario(FuncionarioDto funcionarioDto)
        {
            if (string.IsNullOrEmpty(funcionarioDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(funcionarioDto.CPF)) throw new Exception("Necessário preencher o campo CPF");
            if (!Regex.IsMatch(funcionarioDto.CPF, @"^\d{3}[.]\d{3}[.]\d{3}[-]\d{2}$")) throw new Exception("CPF em formato inválido   EX: xxx.xxx.xxx-xx");

            if (string.IsNullOrEmpty(funcionarioDto.Telefone)) throw new Exception("Necessário preencher o campo telefone");
            if (!Regex.IsMatch(funcionarioDto.Telefone, @"^\d{6,7}[-]?\d{4}$")) throw new Exception("Telefone em formato inválido   EX:1199999-9999");
            if (string.IsNullOrEmpty(funcionarioDto.Endereco)) throw new Exception("Necessário preencher o campo endereço");
            if (funcionarioDto.Cargo < 0) throw new Exception("Necessário preencher o campo cargo");

            Funcionarios funcionario;
            if (funcionarioDto.Id == 0)
            {
                var cpf = _funcionarioRepository.ObtemCpf(funcionarioDto.CPF) ;
                if (cpf == null)
                {
                    funcionario = new Funcionarios
                    {
                        Nome = funcionarioDto.Nome,
                        CPF = funcionarioDto.CPF,
                        Telefone = funcionarioDto.Telefone,
                        Endereco = funcionarioDto.Endereco,
                        Cargo = (int)funcionarioDto.Cargo
                    };

                    funcionarioDto.Id = _funcionarioRepository.AddFuncionario(funcionario);
                }
                else throw new Exception("CPF já cadastrado");
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

        public string DeletaFuncionario(int id)
        {
            var funcionario = _funcionarioRepository.GetFuncionarioId(id) ?? throw new Exception("Funcionario não encontrado");
            var venda = _vendaRepository.ObterFuncionarioVenda(funcionario.Id);
            if (venda != null) throw new Exception("Não é possivel apagar Vendedor cadastrado em uma venda");

            var nome = funcionario.Nome;
            _funcionarioRepository.Excluir(funcionario);
            return nome;
        }

        public IList<ExibeFuncionarioDto> ExibeFuncionarios(string nome)
        {
            var funcionarios = _funcionarioRepository.GetFuncionarios(nome);
            return funcionarios;
        }

        public ExibeFuncionarioDto ExibePorId(int id)
        {
            var funcionario = _funcionarioRepository.GetFuncionarioId(id) ?? throw new Exception("Funcionario não encontrado");
            var cargo = new EnumCargoModel();
            return new ExibeFuncionarioDto
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone,
                Endereco = funcionario.Endereco,
                Cargo = cargo.GetAtributo((EnumCargo)funcionario.Cargo).Nome
            };
        }

        public IList<EnumCargoModel> GetCargos()
        {
            var cargos = new EnumCargoModel().MostraCargos();

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
