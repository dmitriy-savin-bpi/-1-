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

                if (input?.ToLower() == "exit") break;
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Empty string!\n");
                    continue;
                }

                Console.WriteLine(CheckBrackets(input) ? "CORRECT\n" : "INCORRECT\n");
            }
        }

        static bool CheckBrackets(string expr)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char c in expr)
            {
                if (c == '(' || c == '[' || c == '{')
                    stack.Push(c);
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (stack.Count == 0) return false;

                    char open = stack.Pop();

                    if (c == ')' && open != '(') return false;
                    if (c == ']' && open != '[') return false;
                    if (c == '}' && open != '{') return false;
                }
            }

            return stack.Count == 0;
        }
    }
}
