using System;
using System.Diagnostics;
using System.Linq;

namespace Part2_Variant5
{
    class Program
    {
        // 1. Сортировка подсчётом (Counting Sort)
        static int[] CountingSort(int[] array) // O(n+k), O(n+k), O(n+k), память О(k) // Зависит от размера массива и диапазона чисел
        {
            if (array.Length <= 1) return array;

            int max = array.Max();
            int min = array.Min();
            int range = max - min + 1;

            int[] count = new int[range]; //массив-счётчик
            int[] output = new int[array.Length]; //массив для результата

            for (int i = 0; i < array.Length; i++) //раз встр
            {
                count[array[i] - min]++; //увел счётчик
            }

            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1]; //каж эл сумм пред
            }

            for (int i = array.Length - 1; i >= 0; i--) //форм отсор массив
            {
                output[count[array[i] - min] - 1] = array[i];
                count[array[i] - min]--;
            }

            return output;
        }

        // 2. Сортировка пузырьком (Bubble Sort) // O(n), O(n'2), O(n'2), память О(1) //линейно (увел массив в 2 раза - время выросло в 2 раза, либо растёт быстро (увеличили массив в 10 раз - время выросло в 100 раз)
        static int[] BubbleSort(int[] arr)
        {
            int[] result = (int[])arr.Clone();
            int n = result.Length; //запом длину

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (result[j] > result[j + 1]) //пред бол след = меняем 
                    {
                        int temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }
            return result;
        }

        // 3. Пирамидальная сортировка (HeapSort) // O(nlogn), O(nlogn), O(nlogn), память О(1) // быстрее линейного, но медленее квадратичного
        static int[] HeapSort(int[] arr)
        {
            int[] result = (int[])arr.Clone();
            int n = result.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(result, n, i);
            }

            for (int i = n - 1; i > 0; i--) //извл эл из кучи
            {
                int temp = result[0];
                result[0] = result[i];
                result[i] = temp;

                Heapify(result, i, 0);
            }
            return result;
        }

        static void Heapify(int[] arr, int n, int i) //метод восст кучи
        {
            int largest = i;
            int left = 2 * i + 1; //лев потомок
            int right = 2 * i + 2; //прав потомок

            if (left < n && arr[left] > arr[largest])
                largest = left;

            if (right < n && arr[right] > arr[largest])
                largest = right;

            if (largest != i) //если наиб эл - не корень
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                Heapify(arr, n, largest);
            }
        }

        static int get_value(string message) //метод полож числа
        {
            int number = -1;
            bool flag = false;
            while (!flag)
            {
                Console.Write(message);
                string data = Console.ReadLine();
                try
                {
                    number = int.Parse(data);
                    if (number > 0)
                        flag = true;
                    else
                        Console.WriteLine("Число должно быть больше 0");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return number;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== ВАРИАНТ 5 ===");
            Console.WriteLine("Алгоритмы: Counting Sort, Bubble Sort, HeapSort\n");

            int n = get_value("Введите длину массива: ");
            int x = get_value("Введите минимальный элемент массива: ");
            int y = get_value("Введите максимальный элемент массива: ");

            if (x > y)
            {
                int temp = x;
                x = y;
                y = temp;
                Console.WriteLine("Минимальное и максимальное значение поменяны местами");
            }

            int[] originalArray = new int[n];
            int[] counting_m = new int[n];
            int[] bubble_m = new int[n];
            int[] heap_m = new int[n];

            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                originalArray[i] = rnd.Next(x, y + 1);
            }

            Array.Copy(originalArray, counting_m, originalArray.Length);
            Array.Copy(originalArray, bubble_m, originalArray.Length);
            Array.Copy(originalArray, heap_m, originalArray.Length);

            Console.WriteLine("\nисходный массив (первые 20 элементов): " + string.Join(", ", originalArray.Take(20)));
            if (n > 20) Console.WriteLine("...");

            Stopwatch stpwatch = new Stopwatch();

            // Counting Sort
            stpwatch.Start();
            int[] counting_result = CountingSort(counting_m);
            stpwatch.Stop();
            Console.WriteLine("\nрезультат CountingSort (первые 20): " + string.Join(", ", counting_result.Take(20)));
            Console.WriteLine("Время работы сортировки подсчётом: " + stpwatch.Elapsed.TotalMilliseconds.ToString() + " мс");

            // Bubble Sort
            stpwatch.Reset();
            stpwatch.Start();
            int[] bubble_result = BubbleSort(bubble_m);
            stpwatch.Stop();
            Console.WriteLine("результат BubbleSort (первые 20): " + string.Join(", ", bubble_result.Take(20)));
            Console.WriteLine("Время работы сортировки пузырьком: " + stpwatch.Elapsed.TotalMilliseconds.ToString() + " мс");

            // Heap Sort
            stpwatch.Reset();
            stpwatch.Start();
            int[] heap_result = HeapSort(heap_m);
            stpwatch.Stop();
            Console.WriteLine("результат HeapSort (первые 20): " + string.Join(", ", heap_result.Take(20)));
            Console.WriteLine("Время работы пирамидальной сортировки: " + stpwatch.Elapsed.TotalMilliseconds.ToString() + " мс");

            Console.WriteLine("\nВсе работы завершены");
            Console.ReadKey();
        }
    }
}