using APIVenda.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Funcionario
{
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public EnumCargo Cargo { get; set; }
        public string CargoDesc { get { return new EnumCargoModel().GetAtributo(this.Cargo)?.Nome; } }
    }

    public class FuncionarioGetDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cargo { get; set; }
    }
}
