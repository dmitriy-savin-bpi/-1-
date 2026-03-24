using System;
using System.Diagnostics;

namespace Part1_Task1
{
    class Program
    {
        public static int exam(string mesg)
        {
            Console.WriteLine(mesg);
            bool flag = false;
            int num = 0;
            while (!flag)
            {
                try
                {
                    num = int.Parse(Console.ReadLine());
                    if (num > 0)
                        flag = true;
                    else
                        Console.WriteLine("Число должно быть больше 0");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return num;
        }

        public static object interpol_met(int[] m, int key)
        {
            object result = "не найдено";
            int low = 0;
            int high = m.Length - 1;

            while (m[low] <= key && m[high] >= key && low < high)
            {
                if (m[low] == m[high])
                    break;
                int middle = low + ((key - m[low]) * (high - low)) / (m[high] - m[low]);
                if (m[middle] < key)
                    low = middle + 1;
                else if (m[middle] > key)
                    high = middle - 1;
                else if (m[middle] == key)
                {
                    result = middle;
                    return result;
                }
            }
            return result;
        }

        public static object binar_met(int[] m, int key)
        {
            object ress = "не найден";
            int high = m.Length - 1, low = 0;
            int middle = (high + low) / 2;

            while (high >= low)
            {
                if (key == m[middle])
                {
                    ress = middle;
                    return ress;
                }
                else if (key < m[middle])
                    high = middle - 1;
                else
                    low = middle + 1;
                middle = (high + low) / 2;
            }
            return ress;
        }

        static void Main(string[] args)
        {
            int n = exam("введите кол-во элементов массива");
            int x = exam("введите минимальное число массива");
            int y = exam("введите максимальное число массива");
            int key = exam("введите искомый элемент");

            if (x > y)
            {
                Console.WriteLine("были заменены минимальный и максимальный параметр");
                int redf = y;
                y = x;
                x = redf;
            }

            int[] m = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                m[i] = rnd.Next(x, y);

            Array.Sort(m);

            Stopwatch stpwatch = new Stopwatch();
            stpwatch.Start();
            Console.WriteLine("бинарный метод");
            object ressult = binar_met(m, key);
            Console.WriteLine("Итоговый результат: " + (ressult));
            stpwatch.Stop();
            Console.WriteLine("Время работы: " + stpwatch.Elapsed.TotalMilliseconds.ToString());

            stpwatch.Reset();
            stpwatch.Start();
            Console.WriteLine("интерполяционный метод ");
            object jo = interpol_met(m, key);
            Console.WriteLine("Итоговый результат: " + jo);
            stpwatch.Stop();
            Console.WriteLine("Время работы: " + stpwatch.Elapsed.TotalMilliseconds.ToString());

            Console.Write("Все работает");
            Console.ReadKey();
        }
    }
}