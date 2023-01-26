using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Fornecedor;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using APIVenda.Data.Dtos.Cliente;

namespace APIVenda.Aplication
{
    public class FornecedorApp
    {
        private readonly FornecedorRepository _fornecedorRepository;

        public FornecedorApp(DataContext context)
        {
            _fornecedorRepository = new FornecedorRepository(context);
        }

        public FornecedorDto SaveFornecedor(FornecedorDto fornecedorDto)
        {
            if (string.IsNullOrEmpty(fornecedorDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(fornecedorDto.Telefone)) throw new Exception("Necessário preencher o campo telefone");
            if (!Regex.IsMatch(fornecedorDto.Telefone, @"^\d{6,7}[-]?\d{4}$")) throw new Exception("Telefone em formato inválido\nEX:1199999-9999");
            if (string.IsNullOrEmpty(fornecedorDto.Endereco)) throw new Exception("Necessário preencher o campo endereço");

            Fornecedor fornecedor;
            if (fornecedorDto.Id == 0)
            {

                fornecedor = new Fornecedor
                {
                    Nome = fornecedorDto.Nome,
                    Telefone = fornecedorDto.Telefone,
                    Endereco = fornecedorDto.Endereco
                };

                fornecedorDto.Id = _fornecedorRepository.AddFornecedor(fornecedor);
            }
            else
            {
                fornecedor = _fornecedorRepository.GetFornecedorId(fornecedorDto.Id) ?? throw new Exception("Fornecedor não encontrado");

                fornecedor.Nome = fornecedorDto.Nome;
                fornecedor.Endereco = fornecedorDto.Endereco;
                fornecedor.Telefone = fornecedorDto.Telefone;

                _fornecedorRepository.UpdateFornecedor(fornecedor);
            }

            return fornecedorDto;
        }

        public void DeletaFornecedor(int id)
        {
            var cliente = _fornecedorRepository.GetFornecedorId(id) ?? throw new Exception("Fornecedor não encontrado");
            _fornecedorRepository.Excluir(cliente);
        }

        public IList<FornecedorDto> ExibeFornecedores()
        {
            var fornecedor = _fornecedorRepository.GetFornecedores();
            return fornecedor;
        }

        public FornecedorDto ExibePorId(int id)
        {
            var fornecedor = _fornecedorRepository.GetFornecedorId(id) ?? throw new Exception("Fornecedor não encontrado");

            return new FornecedorDto
            {
                Id = fornecedor.Id,
                Endereco = fornecedor.Endereco,
                Nome = fornecedor.Nome,
                Telefone = fornecedor.Telefone
            };
        }
    }
}
