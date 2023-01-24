using System.ComponentModel.DataAnnotations;

namespace APIVenda.Data.Dtos.Cliente
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }
}
