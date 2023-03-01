using APIVenda.Data.Dtos.Cliente;
using APIVenda.Data.Dtos.Fornecedor;
using System;
using System.Text.RegularExpressions;

namespace APIVenda.Utilitarios
{
    public static class Validacoes
    {
        public static void ValidarCampo(string campo, string nomeCampo)
        {
            if (string.IsNullOrEmpty(campo)) 
                throw new Exception($"Necessario preencher o campo {nomeCampo}");
        }

        public static void ValidarTelefone(string telefone)
        {
            if (!Regex.IsMatch(telefone, @"^\d{6,7}[-]?\d{4}$")) 
                throw new Exception("Telefone em formato invalido   EX 1199999-9999 ");
        }

        public static void ValidarDocumento(string documento, EnumDocumento tipo)
        {
            if (tipo == EnumDocumento.CPF)
            {
                if (!Regex.IsMatch(documento, @"^\d{3}[.]\d{3}[.]\d{3}[-]\d{2}$")) 
                    throw new Exception("CPF em formato invalido   EX xxx.xxx.xxx-xx");
            }

            else if (!Regex.IsMatch(documento, @"^\d{2}[.]\d{3}[.]\d{3}[/]\d{4}[-]\d{2}$"))
            {
                throw new Exception("CNPJ em formato invalido EX xx.xxx.xxx/xxxx-xx");
            }
        }
        public static void ValidaPesquisa(object pesquisa,string entidade)
        {
            if(pesquisa==null) throw new Exception($"{entidade} nao encontrado");
        }
        public static void ValidaDeletaComRelacionamento(object pesquisa, string entidade, string atribuido)
        {
            if (pesquisa != null) throw new Exception($"{entidade} cadastrado em um {atribuido},nao possivel exclui-lo");
        }
    }

    public enum EnumDocumento
    {
        CPF = 1,
        CNPJ = 2
    }
}
