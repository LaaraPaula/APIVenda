using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Funcionario;
using System.Collections.Generic;
using System.Linq;
using APIVenda.Data.Enum;
using System.Text.RegularExpressions;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Utilitarios;

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
            Validacoes.ValidarCampo(funcionarioDto.Nome, "nome");
            Validacoes.ValidarCampo(funcionarioDto.Telefone, "telefone");
            Validacoes.ValidarCampo(funcionarioDto.Endereco, "endereço");
            Validacoes.ValidarTelefone(funcionarioDto.Telefone);
            if (funcionarioDto.Cargo < 0) throw new Exception("Necesario preencher o campo cargo");

            Funcionarios funcionario;
            if (funcionarioDto.Id == 0)
            {
                Validacoes.ValidarCampo(funcionarioDto.CPF, "cpf");
                Validacoes.ValidarDocumento(funcionarioDto.CPF, EnumDocumento.CPF);

                var cpf = _funcionarioRepository.ObtemCpf(funcionarioDto.CPF) ;
                if (cpf != null) throw new Exception("CPF ja cadastrado");

                funcionario = new Funcionarios
                {
                    Nome = funcionarioDto.Nome,
                    CPF = funcionarioDto.CPF,
                    Telefone = funcionarioDto.Telefone,
                    Endereco = funcionarioDto.Endereco,
                    Cargo = (int)funcionarioDto.Cargo
                };

                funcionarioDto.Id = _funcionarioRepository.AddFuncionario(funcionario);
                return funcionarioDto;
            }
            funcionario = _funcionarioRepository.GetFuncionarioId(funcionarioDto.Id);
            Validacoes.ValidaPesquisa(funcionario, "Funcionario");

            funcionario.Nome = funcionarioDto.Nome;
            funcionario.Endereco = funcionarioDto.Endereco;
            funcionario.Telefone = funcionarioDto.Telefone;
            funcionario.Cargo = (int)funcionarioDto.Cargo;

            _funcionarioRepository.UpdateFuncionario(funcionario);

            return funcionarioDto;
        }

        public string DeletaFuncionario(int id)
        {
            var funcionario = _funcionarioRepository.GetFuncionarioId(id);
            Validacoes.ValidaPesquisa(funcionario, "Funcionario");

            var venda = _vendaRepository.ObterFuncionarioVenda(funcionario.Id);
            Validacoes.ValidaDeletaComRelacionamento(venda, "Funcionario","venda");

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
            var funcionario = _funcionarioRepository.GetFuncionarioId(id);
            Validacoes.ValidaPesquisa(funcionario, "Funcionario");

            var cargo = new EnumCargoModel();
            return new ExibeFuncionarioDto
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Telefone = funcionario.Telefone,
                Endereco = funcionario.Endereco,
                Cpf = funcionario.CPF,
                Cargo = cargo.GetAtributo((EnumCargo)funcionario.Cargo).Nome
            };
        }

        public IList<EnumCargoModel> GetCargos()
        {
            var cargos = new EnumCargoModel().MostraCargos();

            return cargos;
        }
    }
}
