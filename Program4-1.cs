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

            if (!File.Exists(fileA))
            {
                Console.WriteLine($"Error: file {fileA} not found!");
                Console.WriteLine("Creating test file...");

                string[] testLines = {
                    "Hello world programming is fun",
                    "This is a test file for laboratory work",
                    "Natalia Viktorovna, please count the work"
                };
                File.WriteAllLines(fileA, testLines);
            }

            string[] lines = File.ReadAllLines(fileA);

            Console.WriteLine("Content of file A.txt:");
            Console.WriteLine(new string('-', 50));
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(new string('-', 50));

            Console.Write("\nEnter minimum word length: ");
            int minLength;
            while (!int.TryParse(Console.ReadLine(), out minLength) || minLength < 1)
            {
                Console.Write("Error! Enter a positive integer: ");
            }

            Queue<string> resultQueue = new Queue<string>();

            foreach (string line in lines)
            {
                Queue<string> wordsQueue = new Queue<string>();
                string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    string clean = word.Trim('.', ',', '!', '?', ';', ':', '*', '(', ')', '[', ']', '{', '}', '-');

                    if (clean.Length >= minLength)
                    {
                        wordsQueue.Enqueue(word);
                    }
                }

                resultQueue.Enqueue(string.Join(" ", wordsQueue));
            }

            File.WriteAllLines(fileB, resultQueue);

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
