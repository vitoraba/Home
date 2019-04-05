using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecoMedio
{
    class Program
    {
        static void Main(string[] args)
        {
            //CalculaDistribuicao(GeraMassaDoisClientes());
            CalculaDistribuicao(GeraMassaDoisClientesDuasCombinacoes());
        }

        public static Distribuicao GeraMassa3Clientes()
        {
            Distribuicao dist = new Distribuicao();
            List<Boleta> lstBoletas = new List<Boleta>();
            lstBoletas.Add(new Boleta(1, 1, new decimal(10.01)));
            lstBoletas.Add(new Boleta(2, 1, new decimal(10.02)));
            lstBoletas.Add(new Boleta(3, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(4, 1, new decimal(10.04)));
            lstBoletas.Add(new Boleta(5, 1, new decimal(10.05)));
            lstBoletas.Add(new Boleta(6, 1, new decimal(10.06)));
            lstBoletas.Add(new Boleta(7, 1, new decimal(10.07)));
            lstBoletas.Add(new Boleta(8, 1, new decimal(10.08)));
            lstBoletas.Add(new Boleta(9, 1, new decimal(10.09)));
            lstBoletas.Add(new Boleta(10, 1, new decimal(10.10)));


            dist.Boletas = lstBoletas;


            List<Cliente> lstClientes = new List<Cliente>();
            lstClientes.Add(new Cliente(1, 10, new decimal(10.055)));

            dist.Clientes = lstClientes;

            return dist;
        }

        public static Distribuicao GeraMassaDoisClientes()
        {
            Distribuicao dist = new Distribuicao();
            List<Boleta> lstBoletas = new List<Boleta>();
            lstBoletas.Add(new Boleta(1, 1, new decimal(10.01)));
            lstBoletas.Add(new Boleta(2, 1, new decimal(10.02)));
            lstBoletas.Add(new Boleta(3, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(4, 1, new decimal(10.04)));
            lstBoletas.Add(new Boleta(5, 1, new decimal(10.05)));
            lstBoletas.Add(new Boleta(6, 1, new decimal(10.06)));
            lstBoletas.Add(new Boleta(7, 1, new decimal(10.07)));
            lstBoletas.Add(new Boleta(8, 1, new decimal(10.08)));
            lstBoletas.Add(new Boleta(9, 1, new decimal(10.09)));
            lstBoletas.Add(new Boleta(10, 1, new decimal(10.10)));


            dist.Boletas = lstBoletas;


            List<Cliente> lstClientes = new List<Cliente>();
            lstClientes.Add(new Cliente(1, 5, new decimal(10.03)));//PRIMEIRAS 5
            lstClientes.Add(new Cliente(2, 5, new decimal(10.08)));//DEPOIS AS PROXIMAS 3
            //lstClientes.Add(new Cliente(3, 2, new decimal(10.95)));//AS ULTIMAS 2

            dist.Clientes = lstClientes;

            return dist;
        }

        public static Distribuicao GeraMassaDoisClientesDuasCombinacoes()
        {
            Distribuicao dist = new Distribuicao();
            List<Boleta> lstBoletas = new List<Boleta>();
            lstBoletas.Add(new Boleta(1, 1, new decimal(10.01)));
            lstBoletas.Add(new Boleta(2, 1, new decimal(10.02)));
            lstBoletas.Add(new Boleta(3, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(4, 1, new decimal(10.04)));
            lstBoletas.Add(new Boleta(5, 1, new decimal(10.05)));
            lstBoletas.Add(new Boleta(6, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(7, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(8, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(9, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(10, 1, new decimal(10.03)));
            lstBoletas.Add(new Boleta(11, 1, new decimal(10.06)));
            lstBoletas.Add(new Boleta(12, 1, new decimal(10.07)));
            lstBoletas.Add(new Boleta(13, 1, new decimal(10.08)));
            lstBoletas.Add(new Boleta(14, 1, new decimal(10.09)));
            lstBoletas.Add(new Boleta(15, 1, new decimal(10.10)));



            dist.Boletas = lstBoletas;


            List<Cliente> lstClientes = new List<Cliente>();
            lstClientes.Add(new Cliente(1, 5, new decimal(10.03)));
            lstClientes.Add(new Cliente(2, 5, new decimal(10.08)));
            lstClientes.Add(new Cliente(3, 5, new decimal(10.03)));                       

            dist.Clientes = lstClientes;

            return dist;
        }


        public static void CalculaDistribuicao(Distribuicao dist)
        {
            Resultado resultado = new Resultado();
            resultado.NotIncluded.AddRange(dist.Boletas);

            List<Resultado> lstResultadoAnterior = new List<Resultado>();

            bool first = true;
            foreach (var item in dist.Clientes)
            {
                List<Resultado> resultadoDesteFilhote = new List<Resultado>();
                if (first)
                {
                    CalculateNextFilhote(resultado, resultadoDesteFilhote, item);
                    first = false;
                }
                else
                {
                    foreach (var resultadoAnterior in lstResultadoAnterior)
                    {
                        CalculateNextFilhote(resultadoAnterior, resultadoDesteFilhote, item);
                    }
                }
                if (resultadoDesteFilhote.Count == 0)
                    break;

                lstResultadoAnterior = resultadoDesteFilhote;
            }

        }

        public static void CalculateNextFilhote(Resultado atual, List<Resultado> resultadosDesteFilhote, Cliente cliente)
        {
            CalculateFilhote(cliente, 0, 0, new List<Boleta>(), atual.NotIncluded, 0, atual, resultadosDesteFilhote);
        }

        public static void CalculateFilhote(Cliente cliente, decimal currentAveragePrice, decimal currentQtty, List<Boleta> included, List<Boleta> notIncluded, int startIndex, Resultado atual, List<Resultado> resultadosDesteFilhote)
        {
            for (int index = startIndex; index < notIncluded.Count; index++)
            {
                Boleta nextValue = notIncluded[index];
                decimal newAvgPrice = (((currentAveragePrice * currentQtty) + nextValue.Preco) / (currentQtty + new decimal(1.0)));
                decimal newQtty = currentQtty + 1;

                if (newAvgPrice == cliente.GoalAverage && cliente.GoalQtty == newQtty)
                {                    
                    List<Boleta> newResult = new List<Boleta>(included);
                    newResult.Add(nextValue);

                    Resultado resultadoDesteFilhote = new Resultado();
                    resultadoDesteFilhote.Included.AddRange(newResult);
                    resultadoDesteFilhote.Included.AddRange(atual.Included);
                    resultadoDesteFilhote.NotIncluded.AddRange(notIncluded);
                    resultadoDesteFilhote.NotIncluded.RemoveAt(index);
                    foreach (var item in atual.Distribuicao)
                    {
                        resultadoDesteFilhote.Distribuicao.Add(item.Key, item.Value);
                    }
                    resultadoDesteFilhote.Distribuicao.Add(cliente, newResult);

                    resultadosDesteFilhote.Add(resultadoDesteFilhote);
                }
                else if (newQtty < cliente.GoalQtty)
                {
                    List<Boleta> nextIncluded = new List<Boleta>(included);
                    nextIncluded.Add(nextValue);
                    List<Boleta> nextNotIncluded = new List<Boleta>(notIncluded);
                    nextNotIncluded.Remove(nextValue);
                    CalculateFilhote(cliente, newAvgPrice, newQtty, nextIncluded, nextNotIncluded, startIndex++, atual, resultadosDesteFilhote);
                }
            }
        }



    }
}
