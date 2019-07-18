using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using gfgclasses;

namespace Week_9
{
    public class Permutations
    {
        private readonly Dictionary<char, string> words = new Dictionary<char, string>
        {
            {'a', "a"},
            {'b', "any"},
            {'c', "appear"},
            {'d', "be"},
            {'e', "digit"},
            {'f', "first"},
            {'g', "for"},
            {'h', "in"},
            {'i', "just"},
            {'j', "look"},
            {'k', "numbers"},
            {'l', "on"},
            {'m', "or"},
            {'n', "pattern"},
            {'o', "random"},
            {'p', "reason"},
            {'q', "ten"},
            {'r', "that"},
            {'s', "the"},
            {'t', "to"}
        };

        private void Swap(char[] combination, int pos1, int pos2)
        {
            char c = combination[pos1];
            combination[pos1] = combination[pos2];
            combination[pos2] = c;
        }

        public double SigmoidFunction(UInt64 c, UInt64 n, double T)
        {
            Int64 deltaE = (Int64) n- (Int64)c;
            // Console.WriteLine($"c: {c}");
            // Console.WriteLine($"n: {n}");
            // Console.WriteLine($"deltaE: {deltaE}");
            double toThePower = deltaE / T;
            //BigInteger powerOf = BigInteger.Pow(Math.E, toThePower);
            double probability = 1 / (1 + Math.Pow(Math.E, toThePower));

            return probability;
        }

        public void SimulatedAnnealing()
        {
            Random random = new Random();
            char[] permutation = "abcdefghijklmnopqrst".ToArray();
            
            string va = TryPermutation(new string(permutation));
            if (va == "0") {
							Console.WriteLine("0");
							return;
						}
            UInt64 lastVal = UInt64.Parse(va);

            UInt64 T = 10;
            while (T > 1)
            {
                for (long i = 0; i < 1000000000000; i++)
                {
                    char[] tryPermutation = new string(permutation).ToArray();

                    Move(tryPermutation);

                    string va2 = TryPermutation(new string(tryPermutation));
                    UInt64 newVal = UInt64.Parse(va2);

                    if (lastVal == 0)
                    {
                        Console.WriteLine($"{T} Combination: {new String(permutation)}");
                        Console.WriteLine($"{T} Value      : {lastVal}");
                        Console.WriteLine($"{T} Sentence   : {GetSentence(permutation)}");
                        return;
                    }

                    double probability = SigmoidFunction(lastVal, newVal, T);
                    double chance = random.NextDouble();
                    if (chance <= probability)
                    {
                        permutation = tryPermutation;
                        lastVal = newVal;
                    }
                }
                // Console.WriteLine($"{T} Combination: {new String(permutation)}");
                // Console.WriteLine($"{T} Value      : {lastVal}");
                // Console.WriteLine($"{T} Sentence   : {GetSentence(permutation)}");
                T -= 1;
            }
        }

        public string Get(string uri)
        {
          HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
    request.AutomaticDecompression = DecompressionMethods.GZip |       DecompressionMethods.Deflate;

          using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
          using(Stream stream = response.GetResponseStream())
    using(StreamReader reader = new StreamReader(stream))
          {
            return reader.ReadToEnd();
          }
        }

        // private ulong TryFastPermutation(string permutation)
        // {
        //    return Distance(permutation);
        // }

        // private ulong Distance(string permutation)
        // {
        //    ulong distance = 200831837313463612;
        //    ulong rank = Rank(permutation);
        //    return (ulong)BigInteger.Abs(BigInteger.Subtract(rank, distance));
        // }

        // private ulong Rank(string permuation)
        // {
        //    if (permuation.Count() == 0)
        //    {
        //        return 0;
        //    }

        //   //  ulong x0 = ulong.Parse(new string(new char[] { permuation[0] }));
        //   //  char[] xs = permuation.Substring(1).ToArray();
        //   //  ulong total = Rank(new string(xs));

             
        //     return 0;
        // }      

        private void Move(char[] permuation)
        {
            Random random = new Random();

            int swapVersion = random.Next(0, 120);

            if (swapVersion < 50)
            {
                int random1 = random.Next(0, 20);
                int random2 = random.Next(0, 20);
                Swap(permuation, random1, random2);
            }
            else if (swapVersion >= 50 && swapVersion < 60)
            {
                for (int i = 0; i < 8; i++)
                {
                    int random1 = random.Next(0, 20);
                    int random2 = random.Next(0, 20);
                    Swap(permuation, random1, random2);
                }
            }
            else if (swapVersion >= 60 && swapVersion <= 80)
            {
                int random1 = random.Next(0, 20);
                int random2 = random.Next(0, 20);
                int random3 = random.Next(0, 20);
                int random4 = random.Next(0, 20);
                Swap(permuation, random1, 19);
                Swap(permuation, random2, 18);
                Swap(permuation, random1, 17);
                Swap(permuation, random2, 16);
            } else if (swapVersion >= 81 && swapVersion < 89)
            {
                int random1 = random.Next(10, 20);
                int random2 = random.Next(10, 20);
                int random3 = random.Next(10, 20);
                int random4 = random.Next(10, 20);
                Swap(permuation, random1, 7);
                Swap(permuation, random2, 8);
                Swap(permuation, random3, 9);
                Swap(permuation, random4, 10);
            } else if (swapVersion >= 89 && swapVersion <= 100)
            {
                int random1 = random.Next(15, 20);
                int random2 = random.Next(15, 20);
                int random3 = random.Next(15, 20);
                int random4 = random.Next(15, 20);
                Swap(permuation, random1, 12);
                Swap(permuation, random2, 13);
                Swap(permuation, random1, 14);
                Swap(permuation, random2, 15);
            } else if (swapVersion > 100 && swapVersion < 111)
            {
                int random1 = random.Next(0, 20);
                int random2 = random.Next(0, 20);
                for (int i = random2; i < random2 + 3; i++)
                {
                    if (random1 < 18 && i < 20)
                    {
                        Swap(permuation, random1++, i);
                    }
                }
            } else if (swapVersion > 110 && swapVersion < 120)
            {
                int random1 = random.Next(11, 20);
                int random2 = random.Next(11, 20);
                int random3 = random.Next(11, 20);
                int random4 = random.Next(11, 20);

                Swap(permuation, random1, random2);
                Swap(permuation, random3, random4);
            }
        }

        private string GetSentence(char[] combination)
        {
            string sentence = "";
            foreach (char letter in combination)
            {
                sentence += words[letter] + " ";
            }
            return sentence;
        }

        private string TryPermutation(string combination)
        {
            // string content = Get($"http://firstthreeodds.org/pdq?perm={combination}");

            GFG gfg = new GFG();
	        	return(Math.Abs(gfg.findRank(combination) - 200831837313463612).ToString()); 
        
           // return content;

        }
    }
}