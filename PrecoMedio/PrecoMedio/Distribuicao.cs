using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecoMedio
{
    public class Distribuicao
    {
        public List<Cliente> Clientes { get; set; }

        public List<Boleta> Boletas { get; set; }

        public Dictionary<Cliente, List<Boleta>> Resultado { get; set; }
    }


}
