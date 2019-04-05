using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecoMedio
{
    public class Boleta
    {
        public int Id { get; set; }
        public decimal Qtde { get; set; }
        public decimal Preco { get; set; }

        public Boleta(int id, decimal qtde, decimal preco)
        {
            this.Id = id;
            this.Qtde = qtde;
            this.Preco = preco;
        }
    }
}
