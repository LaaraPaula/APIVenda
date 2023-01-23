using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Funcionario
{
    public class UpdateFuncionarioDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereco { get; set; }
    }
}
