using APIVenda.Data;
using APIVenda.Models;
using APIVenda.Repository;
using System;
using APIVenda.Data.Dtos.Fornecedor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using APIVenda.Utilitarios;

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
            Validacoes.ValidarCampo(fornecedorDto.Nome, "nome");
            Validacoes.ValidarCampo(fornecedorDto.Telefone, "telefone");
            Validacoes.ValidarCampo(fornecedorDto.Endereco, "endereço");
            Validacoes.ValidarTelefone(fornecedorDto.Telefone);


            Fornecedor fornecedor;
            if (fornecedorDto.Id == 0)
            {
                Validacoes.ValidarCampo(fornecedorDto.CNPJ, "CNPJ");
                Validacoes.ValidarDocumento(fornecedorDto.CNPJ, EnumDocumento.CNPJ);

                var cnpj = _fornecedorRepository.ObtemCNPJ(fornecedorDto.CNPJ);

                if (cnpj != null) throw new Exception("CNPJ já cadastrado");

                fornecedor = new Fornecedor
                {
                    Nome = fornecedorDto.Nome,
                    Telefone = fornecedorDto.Telefone,
                    Endereco = fornecedorDto.Endereco,
                    CNPJ = fornecedorDto.CNPJ
                };
                fornecedorDto.Id = _fornecedorRepository.AddFornecedor(fornecedor);
                return fornecedorDto;
            }

            fornecedor = _fornecedorRepository.GetFornecedorId(fornecedorDto.Id);
            Validacoes.ValidaPesquisa(fornecedor, "Fornecedor");

            fornecedor.Nome = fornecedorDto.Nome;
            fornecedor.Endereco = fornecedorDto.Endereco;
            fornecedor.Telefone = fornecedorDto.Telefone;

            _fornecedorRepository.UpdateFornecedor(fornecedor);

            return fornecedorDto;
        }

        public string DeletaFornecedor(int id)
        {
            var fornecedor = _fornecedorRepository.GetFornecedorId(id);
            Validacoes.ValidaPesquisa(fornecedor, "Fornecedor");

            var estoque = _controleEstoqueRepository.GetFornecedorEstoque(fornecedor.Id);
            Validacoes.ValidaDeletaComRelacionamento(estoque, "Fornecedor", "estoque");

            var nome = fornecedor.Nome;
            _fornecedorRepository.Excluir(fornecedor);
            return nome;
        }

        public IList<FornecedorDto> ExibeFornecedores(string nome)
        {
            var fornecedor = _fornecedorRepository.GetFornecedores(nome);
            return fornecedor;
        }

        public FornecedorDto ExibePorId(int id)
        {
            var fornecedor = _fornecedorRepository.GetFornecedorId(id);
            Validacoes.ValidaPesquisa(fornecedor, "Fornecedor");

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
