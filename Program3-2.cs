using System;
using System.Collections;
using System.Collections.Generic;

namespace DoublyLinkedListApp
{
    // УЗЕЛ ДВУНАПРАВЛЕННОГО СПИСКА
    public class DoublyNode<T>
    {
        public DoublyNode(T data)
        {
            Data = data;
            Next = null; //при созд никуда не указывает
            Previous = null; //при созд ниоткуда не указ
        }

        public T Data { get; set; }
        public DoublyNode<T> Next { get; set; }      // ссылка вперёд узел
        public DoublyNode<T> Previous { get; set; }  // ссылка назад узел
    }

    // ДВУНАПРАВЛЕННЫЙ СПИСОК (СОБСТВЕННЫЙ КЛАСС)
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private DoublyNode<T> head;   // первый элемент
        private DoublyNode<T> tail;   // последний элемент
        private int count;            // количество элементов

        public int Count { get { return count; } }
        public DoublyNode<T> First { get { return head; } }
        public DoublyNode<T> Last { get { return tail; } }

        // 1. ДОБАВЛЕНИЕ В КОНЕЦ
        public void AddLast(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            if (head == null) //если список пуст
            {
                head = newNode; //новый вагон станов головой
                tail = newNode; //хвостом
            }
            else //если список не пуст
            {
                newNode.Previous = tail;   // новый ← хвост
                tail.Next = newNode;       // хвост → новый
                tail = newNode;            // новый стал хвостом
            }
            count++;
        }

        // 2. ДОБАВЛЕНИЕ В НАЧАЛО
        public void AddFirst(T data)
        {
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            if (head == null) //если список пуст
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;       // новый → голова
                head.Previous = newNode;   // голова ← новый
                head = newNode;            // новый стал головой
            }
            count++;
        }

        // 3. УДАЛЕНИЕ ПО ЗНАЧЕНИЮ
        public bool Remove(T data)
        {
            DoublyNode<T> current = head;//поиск сначала

            while (current != null)//пока не дошёл до конца
            {
                if (current.Data.Equals(data)) //нашли нуж эл
                {
                    if (count == 1)                    // единственный элемент
                    {
                        head = null;
                        tail = null;
                    }
                    else if (current == head)          // удаляем голову
                    {
                        head = head.Next;
                        head.Previous = null;
                    }
                    else if (current == tail)          // удаляем хвост
                    {
                        tail = tail.Previous;
                        tail.Next = null;
                    }
                    else                               // удаляем из середины
                    {
                        current.Previous.Next = current.Next;//указ на некст
                        current.Next.Previous = current.Previous;//указ предыдущ
                    }
                    count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // 4. ОЧИСТКА СПИСКА
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        // 5. ПОИСК НОМЕРА ЭЛЕМЕНТА (возвращает индекс или -1)
        public int IndexOf(T data)
        {
            DoublyNode<T> current = head; //нач с хэда
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

        // ПРОСМОТР СПИСКА (ПРЯМОЙ ПОРЯДОК)
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
                current = current.Next; // двиг вперёд
                num++;
            }
            Console.WriteLine($"   Всего элементов: {count}");
        }

        // ПРОСМОТР В ОБРАТНОМ ПОРЯДКЕ (для наглядности)
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
                current = current.Previous; //двиг назад
                num--;
            }
        }

        // для поддержки foreach
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

    // ГЛАВНАЯ ПРОГРАММА
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=".PadRight(70, '='));
            Console.WriteLine("ДВУНАПРАВЛЕННЫЙ СПИСОК - ВАРИАНТ 7");
            Console.WriteLine("=".PadRight(70, '='));

            DoublyLinkedList<string> list = new DoublyLinkedList<string>();

            // ВВОД ПРЕДЛОЖЕНИЙ (вопросительные отменяют предыдущее)
            Console.WriteLine("\nВВОД ПРЕДЛОЖЕНИЙ (exit - закончить, ? - отмена):");
            Console.WriteLine("-".PadRight(70, '-'));

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input == "exit") break;

                // если предложение заканчивается на "?" - отменяем последнее
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

            // ИНТЕРАКТИВНОЕ МЕНЮ
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
                    case "1": // добавление в конец
                        Console.Write("Введите значение: ");
                        list.AddLast(Console.ReadLine());
                        Console.WriteLine("   [ДОБАВЛЕНО в конец]");
                        break;

                    case "2": // добавление в начало
                        Console.Write("Введите значение: ");
                        list.AddFirst(Console.ReadLine());
                        Console.WriteLine("   [ДОБАВЛЕНО в начало]");
                        break;

                    case "3": // удаление по значению
                        Console.Write("Введите значение для удаления: ");
                        string val = Console.ReadLine();
                        if (list.Remove(val))
                            Console.WriteLine($"   [УДАЛЕНО] {val}");
                        else
                            Console.WriteLine($"   [НЕ НАЙДЕНО] {val}");
                        break;

                    case "4": // очистка списка
                        list.Clear();
                        Console.WriteLine("   [СПИСОК ОЧИЩЕН]");
                        break;

                    case "5": // поиск номера элемента
                        Console.Write("Введите значение для поиска: ");
                        string search = Console.ReadLine();
                        int idx = list.IndexOf(search);
                        if (idx != -1)
                            Console.WriteLine($"   [НАЙДЕН] на позиции {idx} (индекс {idx})");
                        else
                            Console.WriteLine($"   [НЕ НАЙДЕН] {search}");
                        break;

                    case "6": // показать список (прямой порядок)
                        Console.WriteLine("\nСПИСОК (от начала):");
                        list.PrintForward();
                        break;

                    case "7": // показать список (обратный порядок)
                        Console.WriteLine("\nСПИСОК (от конца):");
                        list.PrintBackward();
                        break;

                    case "0": // выход
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