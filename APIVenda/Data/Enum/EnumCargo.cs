using System;
using System.Collections.Generic;
using System.Linq;

namespace APIVenda.Data.Enum
{
    public enum EnumCargo
    {
        Chefe = 1, 
        Gerente = 2, 
        Vendedor = 3, 
        Atendente = 4, 
        Telefonista = 5,
        GerenteComercial = 6
    }

    public class EnumCargoAtributo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

    }
    public static class EnumCargoModel
    {
        public static List<EnumCargoAtributo> GetAtributo()
        {
            var cargos = System.Enum.GetValues(typeof(EnumCargo)).Cast<EnumCargo>().Select(e => new EnumCargoAtributo
            {
                Id = ((int)e),
                Nome = e.ToString()
            });

            return cargos.ToList();
        }
    }
}
