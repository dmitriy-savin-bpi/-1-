using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Part1_Task2
{
    class Program
    {
        static string exam_1(string mesg)
        {
            Console.WriteLine(mesg);
            bool flag = false;
            string words = "";
            while (!flag)
            {
                try
                {
                    words = (Console.ReadLine());
                    if (words.Length > 0)
                        flag = true;
                    else
                        Console.WriteLine("вы не ввели текст");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return words;
        }

        static int[] Prefix_f(string words)
        {
            int n = words.Length;
            int[] p = new int[n];

            for (int i = 1; i < n; i++)
            {
                int j = p[i - 1];

                while (j > 0 && words[i] != words[j])
                {
                    j = p[j - 1];
                }

                if (words[i] == words[j])
                {
                    j++;
                }

                p[i] = j;
            }
            return p;
        }

        static object Knut_Mor_Prat(string words, string word)
        {
            int[] p = Prefix_f(words);
            int j = 0;
            object ress = "НЕ НАЙДЕН";

            for (int i = 0; i < words.Length; i++)
            {
                while (j > 0 && words[i] != word[j])
                    j = p[j - 1];

                if (words[i] == word[j])
                    j++;

                if (j == word.Length)
                {
                    ress = i - word.Length + 1;
                    return ress;
                }

                string wd = "_";
                int dlin = i;

                if (j > 0)
                {
                    wd = "";
                }

                if (j > 1)
                {
                    dlin -= j - 1;
                }

                for (int u = 0; u < j; u++)
                {
                    wd += word[u];
                }

                Console.WriteLine(words);
                Console.WriteLine(new string(' ', dlin) + wd);
            }
            return ress;
        }

        static object Boyer_M(string words, string word)
        {
            object ress_1 = "не найдено";
            Dictionary<char, int> last_vhog = new Dictionary<char, int>();

            for (int i = 0; i < word.Length; i++)
            {
                last_vhog[word[i]] = i;
            }

            Console.WriteLine(words);
            Console.WriteLine(word);

            for (int i = 0; i <= words.Length - word.Length;)
            {
                int j = word.Length - 1;
                while (j >= 0 && words[i + j] == word[j]) j--;

                if (j < 0)
                {
                    ress_1 = i;
                    return ress_1;
                }

                char no_char = words[i + j];
                int shift = last_vhog.ContainsKey(no_char) ? j - last_vhog[no_char] : j + 1;
                i += shift;
                string pp = new string(' ', i) + word;

                Console.WriteLine(words);
                Console.WriteLine(pp);
            }
            return ress_1;
        }

        static void Main(string[] args)
        {
            string words = exam_1("Введите текст");
            string word = exam_1("Введите словo для поиска ");

            if (word.Length > words.Length)
            {
                Console.WriteLine("Длинна слова превышает длину текста.\n Произведена замена текста на слова и слова на текст соответственно");
                string red_f = word;
                word = words;
                words = red_f;
            }

            Stopwatch stpwatch = new Stopwatch();
            stpwatch.Start();
            object index_1 = Knut_Mor_Prat(words, word);
            Console.WriteLine("Индекс по методу КМП");
            Console.WriteLine(index_1);
            stpwatch.Stop();
            Console.WriteLine("Время работы: " + stpwatch.Elapsed.TotalMilliseconds.ToString());

            stpwatch.Reset();
            stpwatch.Start();
            object index_2 = Boyer_M(words, word);
            Console.WriteLine("Индекс по методу Боуэра-Мура");
            Console.WriteLine(index_2);
            Console.WriteLine("Время работы: " + stpwatch.Elapsed.TotalMilliseconds.ToString());
            Console.ReadKey();
        }
    }
}