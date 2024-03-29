﻿using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Models;
using APIVenda.Repository;
using APIVenda.Utilitarios;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace APIVenda.Aplication
{
    public class ClienteApp
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly VendaRepository _vendaRepository;

        public ClienteApp(DataContext context)
        {
            _clienteRepository = new ClienteRepository(context);
            _vendaRepository = new VendaRepository(context);
        }

        public (ClienteDto, string) SaveClient(ClienteDto clienteDto)
        {
            Validacoes.ValidarCampo(clienteDto.Nome, "nome");
            Validacoes.ValidarCampo(clienteDto.Telefone, "telefone");
            Validacoes.ValidarCampo(clienteDto.Endereco, "endereço");
            Validacoes.ValidarTelefone(clienteDto.Telefone);

            Cliente cliente;
            var tipo = "Editar";
            if (clienteDto.Id == 0)
            {
                tipo = "Cadastro";
                Validacoes.ValidarCampo(clienteDto.CPF, "cpf");
                Validacoes.ValidarDocumento(clienteDto.CPF, EnumDocumento.CPF);

                var cpf = _clienteRepository.ObtemCPF(clienteDto.CPF);
                if (cpf != null) throw new Exception("CPF ja cadastrado");

                cliente = new Cliente
                {
                    Nome = clienteDto.Nome,
                    CPF = clienteDto.CPF,
                    Telefone = clienteDto.Telefone,
                    Endereco = clienteDto.Endereco
                };
                clienteDto.Id = _clienteRepository.AddCliente(cliente);
                return (clienteDto,tipo);

            }
            cliente = _clienteRepository.GetClienteId(clienteDto.Id);
            Validacoes.ValidaPesquisa(cliente, "Cliente");

            cliente.Nome = clienteDto.Nome;
            cliente.Endereco = clienteDto.Endereco;
            cliente.Telefone = clienteDto.Telefone;

            _clienteRepository.UpdateCliente(cliente);

            return (clienteDto,tipo);
        }

        public IList<ClienteDto> ExibeClientes(string nome)
        {
            var clientes = _clienteRepository.GetClientes(nome);
            return clientes;
        }

        public ClienteDto ExibePorId(int id)
        {
            var cliente = _clienteRepository.GetClienteId(id);
            Validacoes.ValidaPesquisa(cliente, "Cliente");

            return new ClienteDto
            {
                Id = cliente.Id,
                CPF = cliente.CPF,
                Endereco = cliente.Endereco,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };
        }

        public string DeletaCliente(int id)
        {
            var cliente = _clienteRepository.GetClienteId(id);
            Validacoes.ValidaPesquisa(cliente, "Cliente");

            var venda = _vendaRepository.ObterClienteVenda(cliente.Id);
            Validacoes.ValidaDeletaComRelacionamento(venda, "Cliente","venda");
            
            var nome = cliente.Nome;
            _clienteRepository.Excluir(cliente);
            return nome;
        }
    }
}
