using System;

namespace Variant20_Lab2
{
    class Program
    {
        // Рекурсивная функция для нахождения максимума
        static int FindMax(int[] arr, int n)
        {
            if (n == 1) //если остался 1 элемент он и будет максимальный
                return arr[0]; //первый элемент массива

            int maxPrev = FindMax(arr, n - 1); //вызываем ту же ф-ю, но уменьш. на 1 кол-во элементов (рекурсивный вызов)
            return arr[n - 1] > maxPrev ? arr[n - 1] : maxPrev; //срав с макс пред части, возвр пред знач
        }

        // Универсальная функция для безопасного ввода целого числа
        static void SafeInput(string prompt, out int value, bool positiveOnly = false)
        {
            value = 0;
            bool valid = false; //флаг коррект ввода

            while (!valid) //пока ввод не станет корр 
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value)) //проверка явл целым числом
                {
                    if (!positiveOnly || value > 0) //если не треб полож чило, усл сразу истинно
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

            // Ввод размера массива
            SafeInput("Введите размер массива: ", out int n, true);

            int[] array = new int[n]; //создание массива размером n

            // Ввод элементов массива
            Console.WriteLine("Введите элементы массива:");
            for (int i = 0; i < n; i++) //от 0 до n-1
            {
                SafeInput($"Элемент [{i + 1}]: ", out array[i]); //интерполяция строк и сохр введ число в i-й эл массива
            }

            // Вызов рекурсивной функции и вывод результата
            int maxElement = FindMax(array, n); //вызов рекурс ф-ии
            Console.WriteLine($"\nМаксимальный элемент в массиве: {maxElement}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}