using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Funcionario
{
    public class CreateFuncionarioDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public int Cargo { get; set; }
    }
}
