using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecoMedio
{
    public class Resultado
    {
        public List<Boleta> Included { get; set; }

        public List<Boleta> NotIncluded { get; set; }

        public Dictionary<Cliente, List<Boleta>> Distribuicao { get; set; }

        public Resultado()
        {
            Included = new List<Boleta>();
            NotIncluded = new List<Boleta>();
            Distribuicao = new Dictionary<Cliente, List<Boleta>>();
        }
    }
}
