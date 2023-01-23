using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Cliente
{
    public class UpdateClienteDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereco { get; set; }
    }
}
