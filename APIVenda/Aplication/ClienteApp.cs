﻿using APIVenda.Data;
using APIVenda.Data.Dtos.Cliente;
using APIVenda.Models;
using APIVenda.Repository;
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

        public ClienteDto SaveClient(ClienteDto clienteDto)
        {
            if (string.IsNullOrEmpty(clienteDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(clienteDto.CPF)) throw new Exception("Necessário preencher o campo CPF");
            if (!Regex.IsMatch(clienteDto.CPF, @"^\d{3}[.]\d{3}[.]\d{3}[-]\d{2}$")) throw new Exception("CPF em formato inválido   EX: xxx.xxx.xxx-xx");

            if (string.IsNullOrEmpty(clienteDto.Telefone)) throw new Exception("Necessário preencher o campo telefone");
            if (!Regex.IsMatch(clienteDto.Telefone, @"^\d{6,7}[-]?\d{4}$")) throw new Exception("Telefone em formato inválido   EX:1199999-9999");
            if (string.IsNullOrEmpty(clienteDto.Endereco)) throw new Exception("Necessário preencher o campo endereço");

            Cliente cliente;
            if (clienteDto.Id == 0)
            {

                var cpf = _clienteRepository.ObtemCPF(clienteDto.CPF);
                if (cpf == null)
                {
                    cliente = new Cliente
                    {
                        Nome = clienteDto.Nome,
                        CPF = clienteDto.CPF,
                        Telefone = clienteDto.Telefone,
                        Endereco = clienteDto.Endereco
                    };
                    clienteDto.Id = _clienteRepository.AddCliente(cliente);
                }
                else throw new Exception("CPF já cadastrado");
            }
            else
            {
                cliente = _clienteRepository.GetClienteId(clienteDto.Id) ?? throw new Exception("Cliente não encontrado");

                cliente.Nome = clienteDto.Nome;
                cliente.Endereco = clienteDto.Endereco;
                cliente.Telefone = clienteDto.Telefone;

                _clienteRepository.UpdateCliente(cliente);
            }

            return clienteDto;
        }

        public IList<ClienteDto> ExibeClientes()
        {
            var clientes = _clienteRepository.GetClientes();
            return clientes;
        }

        public ClienteDto ExibePorId(int id)
        {
            var cliente = _clienteRepository.GetClienteId(id) ?? throw new Exception("Cliente não encontrado");

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
            var cliente = _clienteRepository.GetClienteId(id) ?? throw new Exception("Cliente não encontrado");
            var venda = _vendaRepository.ObterClienteVenda(cliente.Id);

            if (venda != null) throw new Exception("Não é possível excluir o cliente pois ele pertence a uma venda");
            var nome = cliente.Nome;
            _clienteRepository.Excluir(cliente);
            return nome;
        }
    }
}
