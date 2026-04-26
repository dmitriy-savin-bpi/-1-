using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab4_1_SavinDA
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileA = "A.txt";
            string fileB = "B.txt";

            Console.WriteLine("=== VARIANT 3 ===");
            Console.WriteLine();

            // Проверка существования файла A.txt
            if (!File.Exists(fileA))
            {
                Console.WriteLine($"Error: file {fileA} not found!");
                Console.WriteLine("Creating test file...");

                // Создаём тестовый файл на английском
                string[] testLines = {
                    "Hello world programming is fun",
                    "This is a test file for laboratory work",
                    "Natalia Viktorovna, please count the work"
                };
                File.WriteAllLines(fileA, testLines);
            }

            // Чтение файла
            string[] lines = File.ReadAllLines(fileA);

            Console.WriteLine("Content of file A.txt:");
            Console.WriteLine(new string('-', 50)); //Создаёт строку из 50 дефисов (разделитель)
            foreach (string line in lines) //перебираем каждую строку из массива
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(new string('-', 50));//Ещё один разделитель

            Console.Write("\nEnter minimum word length: ");
            int minLength;
            while (!int.TryParse(Console.ReadLine(), out minLength) || minLength < 1) //Пытается преобразовать ввод в число.Если не число - true(ошибка) и провер, что положительноЕ
            {
                Console.Write("Error! Enter a positive integer: ");
            }

            // Процесс с использованием очереди
            Queue<string> resultQueue = new Queue<string>();//созд очерель для результата

            foreach (string line in lines)//Начало внешнего цикла по строкам файла
            {
                Queue<string> wordsQueue = new Queue<string>(); //Создание очереди для слов текущей строки
                string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);//Разбиение строки на слова

                foreach (string word in words)//Внутренний цикл по словам
                {
                    // Очистить от знаков препинания
                    string clean = word.Trim('.', ',', '!', '?', ';', ':', '*', '(', ')', '[', ']', '{', '}', '-');

                    if (clean.Length >= minLength)//Проверка длины и добавление в очередь
                    {
                        wordsQueue.Enqueue(word);
                    }
                }

                resultQueue.Enqueue(string.Join(" ", wordsQueue));//Склеивание очереди в строку
            }

            // Написать результат
            File.WriteAllLines(fileB, resultQueue);//запись в В

            Console.WriteLine("\nResult in file B.txt (words length >= {0}):", minLength);
            Console.WriteLine(new string('-', 50));
            foreach (string line in resultQueue)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}