using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3.Algorytmy
{
    public class Letter
    {
        public int seq { get; set; } // pozycja w wyrazie
        public int value { get; set; } //wartośc litery w ascii -64
        public Letter()
        {
            seq = -1;
            value = -1;
        }
    }
    class Macierzowe2b
    {

        private string M;
        private string key;
        private int il;
        private string result="";
        private List<int> order = new List<int>();
        private List<string> Mtab = new List<string>();
        private List<string> Mtab2 = new List<string>();
        public Macierzowe2b(string M, string key)
        {
            this.M = M.Replace(" ", "");
            this.key = key;
            il = key.Length;
            order = Order();
            Mtab = Matching();
        }

        public List<int> Order()
        {
            List<Letter> order2 = new List<Letter>();
            for (int i = 0; i < key.Length; i++)
            {
                Letter l = new Letter();
                if (((int)key[i] >= 65
                && (int)key[i] <= 90))
                {
                    l.value = (int)key[i] - 64;
                    l.seq = i + 1;
                    //Console.WriteLine(l.value+" "+ l.seq);
                    order2.Add(l);
                }
            }
            order2.Sort(delegate (Letter p1, Letter p2)
            {
                return p1.value.CompareTo(p2.value);
            });
            for (int i = 0; i < key.Length; i++)
            {
                order.Add(0);
                order2[i].value = i + 1;
            }

            for (int i = 0; i < key.Length; i++)
            {
                order[order2[i].seq - 1] = order2[i].value;
            }
            return order;
        }

        public List<string> Matching()
        {
            for (int i = 0; i < il; i++) Mtab.Add("");
            for (int i = 0; i < M.Length; i = i + il)
            {
                for (int y = 0; y < il; y++)
                {
                    if (M.Length > (i + y))
                    {
                        if ((int)M[i + y] == 32) i++;
                        //Console.WriteLine(M[i + y]);
                        String x = Mtab[y] + M[i + y];
                        Mtab[y] = x;
                    }
                }
                //Console.WriteLine(i);
            }
            return Mtab;
        }
        public string Encrypt()
        {
            foreach (int o in order)
            {
                result = result + Mtab[o - 1] + " ";
            }
            return result;
        }

        public string Decrypt()
        {
            string result = M;
            for (int i = 0; i < il; i++) Mtab2.Add("");
            int u = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if ((int)result[i] == 32) { u++; i++; }
                if (u < il) Mtab2[order[u] - 1] = Mtab2[order[u] - 1] + result[i];
            }

            string result2 = "";
            for (int i = 0; i < il; i++)
            {
                foreach (string z in Mtab2)
                {
                    if (z.Length > i)
                        result2 = result2 + z[i];
                }
            }

            foreach (string z in Mtab2)
            {
                Console.WriteLine(z);
            }

            //Console.WriteLine(result2);
            return result2;
        }
    }
}
