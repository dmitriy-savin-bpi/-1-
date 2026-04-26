using System;
using System.Collections.Generic;

namespace Lab4_2_SavinDA
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("=== STACK VARIANT 5 ===");
            Console.WriteLine("Check bracket balance: (), [], {}");
            Console.WriteLine("Type 'exit' to quit\n");

            while (true)
            {
                Console.Write("Enter expression: ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit") break;      // Выход из программы
                if (string.IsNullOrWhiteSpace(input))       // Проверка на пустую строку
                {
                    Console.WriteLine("Empty string!\n");
                    continue;
                }

                // Проверка и вывод результата
                Console.WriteLine(CheckBrackets(input) ? "CORRECT\n" : "INCORRECT\n");
            }
        }

        /// Проверка правильности расстановки скобок с использованием стека
        static bool CheckBrackets(string expr)
        {
            Stack<char> stack = new Stack<char>();  // Стек для открывающих скобок

            foreach (char c in expr) //проход по каждому символу выражения
            {
                // Открывающие скобки -> помещаем в стек
                if (c == '(' || c == '[' || c == '{')
                    stack.Push(c);

                // Закрывающие скобки -> проверяем соответствие
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (stack.Count == 0) return false;  // Проверка, что стек не пуст

                    char open = stack.Pop();  // Извлекаем последнюю открывающую скобку

                    // Проверка соответствия типов скобок
                    if (c == ')' && open != '(') return false;
                    if (c == ']' && open != '[') return false;
                    if (c == '}' && open != '{') return false;
                }
            }

            // Стек должен быть пустым - все скобки закрыты
            return stack.Count == 0;
        }
    }
}