using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Fornecedor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace APIVenda.Aplication
{
    public class FornecedorApp
    {
        private readonly FornecedorRepository _fornecedorRepository;
        private readonly ControleEstoqueRepository _controleEstoqueRepository;

        public FornecedorApp(DataContext context)
        {
            _fornecedorRepository = new FornecedorRepository(context);
            _controleEstoqueRepository = new ControleEstoqueRepository(context);
        }

        public FornecedorDto SaveFornecedor(FornecedorDto fornecedorDto)
        {
            if (string.IsNullOrEmpty(fornecedorDto.Nome)) throw new Exception("Necessário preencher o campo nome");
            if (string.IsNullOrEmpty(fornecedorDto.CNPJ)) throw new Exception("Necessário preencher o campo CNPJ");
            if (!Regex.IsMatch(fornecedorDto.CNPJ, @"^\d{2}[.]\d{3}[.]\d{3}[/]\d{4}[-]\d{2}$")) throw new Exception("CNPJ em formato inválido xx.xxx.xxx/xxxx-xx");

            if (string.IsNullOrEmpty(fornecedorDto.Telefone)) throw new Exception("Necessário preencher o campo telefone");
            if (!Regex.IsMatch(fornecedorDto.Telefone, @"^\d{6,7}[-]?\d{4}$")) throw new Exception("Telefone em formato inválido    EX:1199999-9999");
            if (string.IsNullOrEmpty(fornecedorDto.Endereco)) throw new Exception("Necessário preencher o campo endereço");

            Fornecedor fornecedor;
            if (fornecedorDto.Id == 0)
            {
                var cnpj = _fornecedorRepository.ObtemCNPJ(fornecedorDto.CNPJ);
                if (cnpj == null)
                {
                    fornecedor = new Fornecedor
                    {
                        Nome = fornecedorDto.Nome,
                        Telefone = fornecedorDto.Telefone,
                        Endereco = fornecedorDto.Endereco,
                        CNPJ = fornecedorDto.CNPJ
                    };
                    fornecedorDto.Id = _fornecedorRepository.AddFornecedor(fornecedor);
                }
                else throw new Exception("CNPJ já cadastrado");

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

        public string DeletaFornecedor(int id)
        {
            var fornecedor = _fornecedorRepository.GetFornecedorId(id) ?? throw new Exception("Fornecedor não encontrado");
            var estoque = _controleEstoqueRepository.GetFornecedorEstoque(fornecedor.Id);

            if (estoque != null) throw new Exception("Não é possível deletar fornecedor cadastrado em estoque");

            var nome = fornecedor.Nome;
            _fornecedorRepository.Excluir(fornecedor);
            return nome;
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
                Telefone = fornecedor.Telefone,
                CNPJ = fornecedor.CNPJ
            };
        }
    }
}
