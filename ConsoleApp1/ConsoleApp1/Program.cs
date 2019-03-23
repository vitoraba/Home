using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            Solution sol = new Solution();

            Tree _1 = new Tree();
            Tree _2 = new Tree();
            Tree _3 = new Tree();
            Tree _4 = new Tree();
            Tree _5 = new Tree();
            Tree _6 = new Tree();
            Tree _7 = new Tree();
            Tree _8 = new Tree();
            Tree _9 = new Tree();
            Tree _10 = new Tree();
            Tree _11 = new Tree();

            _1.x = 1;
            _2.x = 2;
            _3.x = 3;
            _4.x = 4;
            _5.x = 5;
            _6.x = 6;
            _7.x = 7;
            _8.x = 8;
            _9.x = 9;
            _10.x = 10;
            _11.x = 11;

            _1.l = _2;
            _1.r = _3;
            _2.l = _4;
            _3.l = _5;
            _3.r = _6;
            _5.l = _7;
            _5.r = _8;
            _6.l = _9;
            _6.r = _10;
            _10.r = _11;

            int i = sol.solution(_1);

            Console.ReadKey();
        }
        teste;
    }

    class Tree
    {
        public int x;
        public Tree l;
        public Tree r;
    };

    class Solution
    {
        public int solution(Tree T)
        {

            return maiorArvoreBinariaPerfeita(T, 0);
        }

        public int maiorArvoreBinariaPerfeita(Tree node, int maiorTamanhoAnterior)
        {
            /* Condicao de parada */
            if (node == null)
            {
                return 0;
            }

            /* É PERFEITA APENAS SE TIVER 2 LADOS OU NAO TIVER NENHUM */
            if ((node.l != null && node.r != null) || (node.l == null && node.r == null))
            {
                int tamanhoEsquerda = maiorArvoreBinariaPerfeita(node.l, maiorTamanhoAnterior);
                int tamanhoDireita = maiorArvoreBinariaPerfeita(node.r, maiorTamanhoAnterior);

                if (tamanhoEsquerda + tamanhoDireita + 1 > maiorTamanhoAnterior)
                {
                    return tamanhoEsquerda + tamanhoDireita + 1;
                }
                else
                    return maiorTamanhoAnterior;
            }

            else
            {
                return 0;
            }
        }
    }



    /*
    class Program
    {
        static void Main(string[] args)
        {
            //            findSchedules(3, 1, "???????");

            openAndClosePrices("26-March-2001", "15-August-2001", "Wednesday");

            Console.ReadKey();
        }


        public static List<int> findSchedulesRecursive(int target, int qtdeADefinir, List<int> parcial, int limiteDiario)
        {
            List<int> retorno = new List<int>();
            if (parcial.Sum(x => x) == target && qtdeADefinir == parcial.Count)
            {
                return parcial;
            }

            int i = 0;


            while (parcial.Count < qtdeADefinir && i <= limiteDiario)
            {
                parcial.Add(i);
                retorno.AddRange(findSchedulesRecursive(target, qtdeADefinir, parcial, limiteDiario));
                parcial.RemoveAt(parcial.Count - 1);
                i++;
            }
            return retorno;
        }

        public static List<string> findSchedules(int workHours, int dayHours, string pattern)
        {
            List<int> posicoesInterrogacao = new List<int>();
            List<string> retorno = new List<string>();

            int qtdeADefinir = 0;
            int qtdeRestante = workHours;

            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '?')
                {
                    qtdeADefinir++;
                    posicoesInterrogacao.Add(i);
                }
                else
                    qtdeRestante = qtdeRestante - Convert.ToInt32(Convert.ToString(pattern[i]));
            }

            if (qtdeRestante == 0)
            {
                retorno.Add(pattern);
                return retorno;
            }

            List<int> combinacoes = findSchedulesRecursive(qtdeRestante, qtdeADefinir, new List<int>(), dayHours);


            string patternNovo = pattern;

            for (int i = 0; i < combinacoes.Count; i++)
            {
                patternNovo = patternNovo.Substring(0, patternNovo.IndexOf('?')) + combinacoes[i] + patternNovo.Substring(patternNovo.IndexOf('?') + 1);

                if (!patternNovo.Contains("?"))
                {
                    retorno.Add(patternNovo);
                    patternNovo = pattern;
                }

            }

            return retorno;
        }


        public class JsonResponse
        {
            public string page { get; set; }
            public string per_page { get; set; }
            public string total { get; set; }
            public int total_pages { get; set; }
            public List<Rate> data { get; set; }
        }

        public class Rate
        {
            public DateTime date { get; set; }
            public string open { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string close { get; set; }
        }

        static void openAndClosePrices(string firstDate, string lastDate, string weekDay)
        {
            DateTime inicio = DateTime.MinValue;
            DateTime fim = DateTime.MinValue;
            DateTime.TryParseExact(firstDate, "d-MMMM-yyyy", null, System.Globalization.DateTimeStyles.None, out inicio);
            DateTime.TryParseExact(lastDate, "d-MMMM-yyyy", null, System.Globalization.DateTimeStyles.None, out fim);

            while (inicio <= fim)
            {

                if (inicio.DayOfWeek.ToString() == weekDay)
                {
                    Boolean nextpage = true;
                    int pagina = 1;
                    while (nextpage)
                    {
                        JsonResponse model = new JsonResponse();
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://jsonmock.hackerrank.com/api/stocks/?date=" + inicio.ToString("d-MMMM-yyyy")+ "&page="+pagina.ToString());

                        WebResponse response = request.GetResponse();
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                            model = JsonConvert.DeserializeObject<JsonResponse>(reader.ReadToEnd());
                        }

                        if (model.data.Count > 0)
                        {
                            foreach (var item in model.data)
                            {
                                //21-February-2000 5874.25 5876.89
                                Console.WriteLine(item.date.ToString("d-MMMM-yyyy") + " " + item.open + " " + item.close);
                            }
                        }

                        if (model.total_pages > pagina)
                        {
                            pagina++;
                        }
                        else
                        {
                            nextpage = false;
                        }
                    }
                }

                inicio = inicio.AddDays(1);
            }
        }

    }*/

}

