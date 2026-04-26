using System;

namespace Variant20_Lab2
{
    class Program
    {
        static int FindMax(int[] arr, int n)
        {
            if (n == 1)
                return arr[0];

            int maxPrev = FindMax(arr, n - 1);
            return arr[n - 1] > maxPrev ? arr[n - 1] : maxPrev;
        }

        static void SafeInput(string prompt, out int value, bool positiveOnly = false)
        {
            value = 0;
            bool valid = false;

            while (!valid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    if (!positiveOnly || value > 0)
                        valid = true;
                    else
                        Console.WriteLine("Ошибка: число должно быть положительным.");
                }
                else
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Поиск максимального элемента в массиве (рекурсия) ===");

            SafeInput("Введите размер массива: ", out int n, true);

            int[] array = new int[n];

            Console.WriteLine("Введите элементы массива:");
            for (int i = 0; i < n; i++)
            {
                SafeInput($"Элемент [{i + 1}]: ", out array[i]);
            }

            int maxElement = FindMax(array, n);
            Console.WriteLine($"\nМаксимальный элемент в массиве: {maxElement}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
