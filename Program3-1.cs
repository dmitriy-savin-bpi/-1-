using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleAlgorithmsApp
{
    // Класс узла
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data; //сохр данные в узле
        }
        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }

    // Односвязный список
    public class LList<T> : IEnumerable<T> //для возможности использ foreach
    {
        Node<T> head; // головной/первый элемент
        Node<T> tail; // последний/хвостовой элемент
        int count;    // количество элементов в списке

        // Добавление элемента в конец
        public void Add(T data) //слож О(1) - мгновенно
        {
            Node<T> node = new Node<T>(data); //узел с перед данными
            if (head == null) //проверка пустой ли список
                head = node;
            else
                tail.Next = node; //если не пуст, тек ласт эл должен указ на новй узел
            tail = node; //нью узел стан ласт
            count++;
        }

        // Добавление в начало
        public void AppendFirst(T data)
        {
            Node<T> node = new Node<T>(data); //созд нью узла
            node.Next = head;
            head = node;
            if (count == 0) //если список пуст, то нью узел также явл и хвостом
                tail = head;
            count++; //счёт увел
        }

        // Добавление в определенную позицию
        public void AddAt(int index, T data) // О(n) - нужно пройти до нуж позиции
        {
            if (index < 0 || index > count) //индекс не мб отриц или больше списка
                throw new ArgumentOutOfRangeException("index");

            if (index == 0) //если встав в начало
            {
                AppendFirst(data);
                return;
            }

            if (index == count) //если встав в конец
            {
                Add(data);
                return;
            }

            Node<T> newNode = new Node<T>(data); //если встав в середину списка
            Node<T> current = head;
            for (int i = 0; i < index - 1; i++)
                current = current.Next;

            newNode.Next = current.Next;
            current.Next = newNode; //пред ущел теперь указ на новый
            count++;
        }

        // Удаление по значению
        public bool Remove(T data) //О(n) - нужно найти элемент
        {
            if (head == null)
                return false;

            Node<T> current = head; //нач с головы
            Node<T> previous = null; //пред узел

            while (current != null) //проход по списку
            {
                if (current.Data.Equals(data)) //срав с искомым
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;
                        if (head == null)
                            tail = null;
                    }
                    count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        // Удаление по номеру
        public bool RemoveAt(int index) //О(n)
        {
            if (head == null)
                return false;

            if (index < 0 || index >= count)
                return false;

            if (index == 0)
            {
                head = head.Next;
                if (head == null)
                    tail = null;
                count--;
                return true;
            }

            Node<T> current = head;
            for (int i = 0; i < index - 1; i++)
                current = current.Next;

            current.Next = current.Next.Next;
            if (current.Next == null)
                tail = current;

            count--;
            return true;
        }

        // Очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        // Поиск номера элемента по значению
        public int IndexOf(T data)
        {
            if (head == null)
                return -1;

            Node<T> current = head;
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

        // Проверка наличия элемента
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // Свойства
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        // Просмотр списка
        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Node<T> current = head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        // Реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }

    // Главная программа
    class Program
    {
        static void Main(string[] args)
        {
            // Из массива символов строим список
            char[] chars = { 'A', 'B', 'C', 'D' };
            LList<char> list = new LList<char>();

            // Заполнение из массива
            foreach (char c in chars)
                list.Add(c);

            Console.WriteLine("Начальный список:");
            list.Print();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Меню ---");
                Console.WriteLine("1. Добавить в конец");
                Console.WriteLine("2. Добавить в начало");
                Console.WriteLine("3. Добавить на позицию");
                Console.WriteLine("4. Удалить по значению");
                Console.WriteLine("5. Удалить по номеру");
                Console.WriteLine("6. Очистить список");
                Console.WriteLine("7. Поиск номера элемента");
                Console.WriteLine("8. Просмотр списка");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        if (list.IsEmpty)
                            Console.WriteLine("Список пуст, но вы можете добавить элемент");
                        Console.Write("Введите символ: ");
                        char c1 = Console.ReadLine()[0];
                        list.Add(c1);
                        Console.WriteLine("Добавлено: " + c1);
                        break;

                    case "2":
                        Console.Write("Введите символ: ");
                        char c2 = Console.ReadLine()[0];
                        list.AppendFirst(c2);
                        Console.WriteLine("Добавлено в начало: " + c2);
                        break;

                    case "3":
                        if (list.IsEmpty)
                        {
                            Console.WriteLine("Список пуст. Сначала добавьте элементы через пункт 1 или 2");
                            break;
                        }
                        Console.Write("Введите позицию (0-" + list.Count + "): ");
                        if (int.TryParse(Console.ReadLine(), out int pos))
                        {
                            Console.Write("Введите символ: ");
                            char c3 = Console.ReadLine()[0];
                            try
                            {
                                list.AddAt(pos, c3);
                                Console.WriteLine("Добавлен символ " + c3 + " на позицию " + pos);
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка: неверная позиция");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите число");
                        }
                        break;

                    case "4":
                        if (list.IsEmpty)
                        {
                            Console.WriteLine("Список пуст, нечего удалять");
                            break;
                        }
                        Console.Write("Введите символ для удаления: ");
                        char val = Console.ReadLine()[0];
                        if (list.Remove(val))
                            Console.WriteLine("Удалено: " + val);
                        else
                            Console.WriteLine("Символ '" + val + "' не найден");
                        break;

                    case "5":
                        if (list.IsEmpty)
                        {
                            Console.WriteLine("Список пуст, нечего удалять");
                            break;
                        }
                        Console.Write("Введите номер для удаления (0-" + (list.Count - 1) + "): ");
                        if (int.TryParse(Console.ReadLine(), out int idx))
                        {
                            if (list.RemoveAt(idx))
                                Console.WriteLine("Удалён элемент с номером " + idx);
                            else
                                Console.WriteLine("Неверный номер");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите число");
                        }
                        break;

                    case "6":
                        if (list.IsEmpty)
                        {
                            Console.WriteLine("Список и так пуст");
                        }
                        else
                        {
                            list.Clear();
                            Console.WriteLine("Список очищен");
                        }
                        break;

                    case "7":
                        if (list.IsEmpty)
                        {
                            Console.WriteLine("Список пуст, нечего искать");
                            break;
                        }
                        Console.Write("Введите символ для поиска: ");
                        char search = Console.ReadLine()[0];
                        int found = list.IndexOf(search);
                        if (found != -1)
                            Console.WriteLine("Элемент '" + search + "' найден на позиции " + found);
                        else
                            Console.WriteLine("Элемент '" + search + "' не найден");
                        break;

                    case "8":
                        list.Print();
                        break;

                    case "0":
                        exit = true;
                        Console.WriteLine("Программа завершена");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Введите 0-8");
                        break;
                }
            }
        }
    }
}