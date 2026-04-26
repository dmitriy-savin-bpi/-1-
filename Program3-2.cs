using System;
using System.Collections;
using System.Collections.Generic;

namespace DoublyLinkedListApp
{
    public class DoublyNode<T>
    {
        public DoublyNode(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }

        public T Data { get; set; }
        public DoublyNode<T> Next { get; set; }
        public DoublyNode<T> Previous { get; set; }
    }

    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private DoublyNode<T> head;
        private DoublyNode<T> tail;
        private int count;

        public int Count { get { return count; } }
        public DoublyNode<T> First { get { return head; } }
        public DoublyNode<T> Last { get { return tail; } }

        public void AddLast(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Previous = tail;
                tail.Next = newNode;
                tail = newNode;
            }
            count++;
        }

        public void AddFirst(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            count++;
        }

        public bool Remove(T data)
        {
            DoublyNode<T> current = head;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (count == 1)
                    {
                        head = null;
                        tail = null;
                    }
                    else if (current == head)
                    {
                        head = head.Next;
                        head.Previous = null;
                    }
                    else if (current == tail)
                    {
                        tail = tail.Previous;
                        tail.Next = null;
                    }
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }
                    count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public int IndexOf(T data)
        {
            DoublyNode<T> current = head;
            int index = 0;

            while (current != null)
            {
                if (current.Data.Equals(data))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        public void PrintForward()
        {
            if (head == null)
            {
                Console.WriteLine("   [Список пуст]");
                return;
            }

            DoublyNode<T> current = head;
            int num = 1;
            while (current != null)
            {
                Console.WriteLine($"   {num}. {current.Data}");
                current = current.Next;
                num++;
            }
            Console.WriteLine($"   Всего элементов: {count}");
        }

        public void PrintBackward()
        {
            if (tail == null)
            {
                Console.WriteLine("   [Список пуст]");
                return;
            }

            DoublyNode<T> current = tail;
            int num = count;
            while (current != null)
            {
                Console.WriteLine($"   {num}. {current.Data}");
                current = current.Previous;
                num--;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=".PadRight(70, '='));
            Console.WriteLine("ДВУНАПРАВЛЕННЫЙ СПИСОК - ВАРИАНТ 7");
            Console.WriteLine("=".PadRight(70, '='));

            DoublyLinkedList<string> list = new DoublyLinkedList<string>();

            Console.WriteLine("\nВВОД ПРЕДЛОЖЕНИЙ (exit - закончить, ? - отмена):");
            Console.WriteLine("-".PadRight(70, '-'));

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input == "exit") break;

                if (input.Trim().EndsWith("?"))
                {
                    if (list.Count > 0)
                    {
                        string removed = list.Last.Data;
                        list.Remove(removed);
                        Console.WriteLine($"   [ОТМЕНА] удалено: {removed}");
                    }
                    else Console.WriteLine("   [НЕЧЕГО ОТМЕНЯТЬ]");
                }
                else
                {
                    list.AddLast(input);
                    Console.WriteLine($"   [ДОБАВЛЕНО] {input}");
                }
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n" + "-".PadRight(70, '-'));
                Console.WriteLine("МЕНЮ:");
                Console.WriteLine("1. Добавить в КОНЕЦ");
                Console.WriteLine("2. Добавить в НАЧАЛО");
                Console.WriteLine("3. Удалить по ЗНАЧЕНИЮ");
                Console.WriteLine("4. ОЧИСТИТЬ список");
                Console.WriteLine("5. ПОИСК номера элемента");
                Console.WriteLine("6. Показать список");
                Console.WriteLine("7. Показать список (обратный)");
                Console.WriteLine("0. Выход");
                Console.Write("> ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите значение: ");
                        list.AddLast(Console.ReadLine());
                        Console.WriteLine("   [ДОБАВЛЕНО в конец]");
                        break;

                    case "2":
                        Console.Write("Введите значение: ");
                        list.AddFirst(Console.ReadLine());
                        Console.WriteLine("   [ДОБАВЛЕНО в начало]");
                        break;

                    case "3":
                        Console.Write("Введите значение для удаления: ");
                        string val = Console.ReadLine();
                        if (list.Remove(val))
                            Console.WriteLine($"   [УДАЛЕНО] {val}");
                        else
                            Console.WriteLine($"   [НЕ НАЙДЕНО] {val}");
                        break;

                    case "4":
                        list.Clear();
                        Console.WriteLine("   [СПИСОК ОЧИЩЕН]");
                        break;

                    case "5":
                        Console.Write("Введите значение для поиска: ");
                        string search = Console.ReadLine();
                        int idx = list.IndexOf(search);
                        if (idx != -1)
                            Console.WriteLine($"   [НАЙДЕН] на позиции {idx} (индекс {idx})");
                        else
                            Console.WriteLine($"   [НЕ НАЙДЕН] {search}");
                        break;

                    case "6":
                        Console.WriteLine("\nСПИСОК (от начала):");
                        list.PrintForward();
                        break;

                    case "7":
                        Console.WriteLine("\nСПИСОК (от конца):");
                        list.PrintBackward();
                        break;

                    case "0":
                        exit = true;
                        Console.WriteLine("   [ВЫХОД]");
                        break;

                    default:
                        Console.WriteLine("   [ОШИБКА] выберите 0-7");
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
