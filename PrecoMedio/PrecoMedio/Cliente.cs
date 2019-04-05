using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecoMedio
{
    public class Cliente
    {
        public int Id { get; set; }
        public decimal GoalQtty { get; set; }
        public decimal GoalAverage { get; set; }


        public Cliente(int id, decimal goalQtty, decimal goalAverage)
        {
            this.Id = id;
            this.GoalQtty = goalQtty;
            this.GoalAverage = goalAverage;

        }
    }
}
