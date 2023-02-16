using APIVenda.Data.Enum;

namespace APIVenda.Data.Dtos.Funcionario
{
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public EnumCargo Cargo { get; set; }
    }

    public class ExibeFuncionarioDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cargo { get; set; }
    }
}
