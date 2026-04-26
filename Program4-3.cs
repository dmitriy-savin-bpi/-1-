using System;

namespace Lab4_3_SavinDA
{
    // КЛАСС УЗЛА ДЕКА (ДВУНАПРАВЛЕННЫЙ УЗЕЛ)
    class DequeNode<T>
    {
        public T Data { get; set; }                 // Данные, которые хранит узел (число, строка и т.д.)
        public DequeNode<T> Next { get; set; }      // Ссылка на СЛЕДУЮЩИЙ узел (вперёд)
        public DequeNode<T> Previous { get; set; }  // Ссылка на ПРЕДЫДУЩИЙ узел (назад)

        // Конструктор узла - вызывается при создании нового узла
        public DequeNode(T data)
        {
            Data = data;        // Сохраняем переданные данные
            Next = null;        // Пока нет следующего узла
            Previous = null;    // Пока нет предыдущего узла
        }
    }

    // КЛАСС ДЕК (ДВУНАПРАВЛЕННАЯ ОЧЕРЕДЬ)
    // Реализует структуру данных "дек" - можно добавлять/удалять с обоих концов
    class Deque<T>
    {
        // ПРИВАТНЫЕ ПОЛЯ (скрыты от внешнего кода)
        private DequeNode<T> head;  // Ссылка на ПЕРВЫЙ (головной) элемент дека
        private DequeNode<T> tail;  // Ссылка на ПОСЛЕДНИЙ (хвостовой) элемент дека
        private int count;          // Счётчик количества элементов в деке

        // ПУБЛИЧНЫЕ СВОЙСТВА (доступны извне)
        public int Count { get { return count; } }          // Возвращает количество элементов
        public bool IsEmpty { get { return count == 0; } }   // Проверка, пуст ли дек

        // СВОЙСТВО ДЛЯ ДОСТУПА К ГОЛОВЕ (только для чтения)
        public DequeNode<T> Head { get { return head; } }

        // ДОБАВЛЕНИЕ В НАЧАЛО
        public void AddFront(T data)
        {
            // Создаём новый узел с переданными данными
            DequeNode<T> node = new DequeNode<T>(data);

            // Если дек пуст (нет ни одного элемента)
            if (head == null)
            {
                head = node;   // Новый узел становится головой
                tail = node;   // Новый узел становится хвостом (он единственный)
            }
            else
            {
                // Если дек не пуст
                node.Next = head;      // Новый узел указывает на старую голову (связываем вперёд)
                head.Previous = node;  // Старая голова указывает на новый узел (связываем назад)
                head = node;           // Новый узел становится новой головой
            }
            count++;  // Увеличиваем счётчик элементов
        }

        // ДОБАВЛЕНИЕ В КОНЕЦ 
        public void AddBack(T data)
        {
            // Создаём новый узел с переданными данными
            DequeNode<T> node = new DequeNode<T>(data);

            // Если дек пуст
            if (tail == null)
            {
                head = node;   // Новый узел становится головой
                tail = node;   // Новый узел становится хвостом
            }
            else
            {
                // Если дек не пуст
                tail.Next = node;      // Старый хвост указывает на новый узел
                node.Previous = tail;  // Новый узел указывает на старый хвост
                tail = node;           // Новый узел становится новым хвостом
            }
            count++;  // Увеличиваем счётчик
        }

        // УДАЛЕНИЕ С НАЧАЛА
        // out T value - возвращает удалённое значение через выходной параметр
        public bool RemoveFront(out T value)
        {
            value = default(T);  // Значение по умолчанию (0 для чисел, null для ссылочных типов)

            // Если дек пуст - ничего не удаляем, возвращаем false
            if (head == null)
                return false;

            // Сохраняем данные удаляемого узла
            value = head.Data;

            // Если в деке всего один элемент
            if (head == tail)
            {
                head = null;   // Очищаем голову
                tail = null;   // Очищаем хвост
            }
            else
            {
                // Если элементов несколько
                head = head.Next;      // Новая голова - следующий элемент
                head.Previous = null;  // У новой головы нет предыдущего (обрываем связь)
            }

            count--;  // Уменьшаем счётчик
            return true;  // Успешно удалили
        }

        // УДАЛЕНИЕ С КОНЦА
        public bool RemoveBack(out T value)
        {
            value = default(T);

            // Если дек пуст
            if (tail == null)
                return false;

            // Сохраняем данные удаляемого узла
            value = tail.Data;

            // Если в деке всего один элемент
            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                // Если элементов несколько
                tail = tail.Previous;  // Новый хвост - предыдущий элемент
                tail.Next = null;      // У нового хвоста нет следующего
            }

            count--;
            return true;
        }

        // ПРОСМОТР ПЕРВОГО ЭЛЕМЕНТА (без удаления)
        public bool PeekFront(out T value)
        {
            value = default(T);
            if (head == null) return false;  // Дек пуст
            value = head.Data;               // Берём данные из головы
            return true;
        }

        // ПРОСМОТР ПОСЛЕДНЕГО ЭЛЕМЕНТА (без удаления)
        public bool PeekBack(out T value)
        {
            value = default(T);
            if (tail == null) return false;  // Дек пуст
            value = tail.Data;               // Берём данные из хвоста
            return true;
        }

        // ОЧИСТКА ДЕКА (удаляем все элементы)
        public void Clear()
        {
            head = null;   // Обнуляем ссылку на голову
            tail = null;   // Обнуляем ссылку на хвост
            count = 0;     // Сбрасываем счётчик
        }

        // ВЫВОД ВСЕХ ЭЛЕМЕНТОВ НА ЭКРАН
        public void Print(string name = "Deque")
        {
            Console.Write($"{name}: ");

            // Если дек пуст
            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            // Идём от головы к хвосту через ссылку Next
            DequeNode<T> current = head;  // Устанавливаем указатель на голову (как q = head в лекции)
            while (current != null)       // Пока не дошли до конца (current != NULL)
            {
                Console.Write($"{current.Data} ");  // Выводим данные узла
                current = current.Next;             // Переходим к следующему узлу (q = q->next)
            }
            Console.WriteLine();
        }
    }

    // ОСНОВНАЯ ПРОГРАММА
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём дек для хранения целых чисел
            Deque<int> deque = new Deque<int>();
            bool running = true;  // Переменная для работы бесконечного цикла

            // ВЫВОД МЕНЮ
            Console.WriteLine("=== DEQUE VARIANT 3 ===");
            Console.WriteLine("1.Add front  2.Add back   3.Remove front  4.Remove back");
            Console.WriteLine("5.Print      6.Clear      7.Split +/-     8.Exit\n");

            // БЕСКОНЕЧНЫЙ ЦИКЛ - программа работает, пока пользователь не выберет "8"
            while (running)
            {
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                // ОПЕРАТОР SWITCH - выбирает действие в зависимости от ввода пользователя
                switch (choice)
                {
                    case "1": // ДОБАВИТЬ В НАЧАЛО
                        Console.Write("Enter number: ");
                        if (int.TryParse(Console.ReadLine(), out int f))
                            deque.AddFront(f);  // Вызываем метод добавления в начало
                        break;

                    case "2": // ДОБАВИТЬ В КОНЕЦ
                        Console.Write("Enter number: ");
                        if (int.TryParse(Console.ReadLine(), out int b))
                            deque.AddBack(b);   // Вызываем метод добавления в конец
                        break;

                    case "3": // УДАЛИТЬ С НАЧАЛА
                        if (deque.RemoveFront(out int removedFront))
                            Console.WriteLine($"Removed: {removedFront}");  // Выводим удалённый элемент
                        else
                            Console.WriteLine("Deque is empty!");  // Дек пуст - ошибка
                        break;

                    case "4": // УДАЛИТЬ С КОНЦА
                        if (deque.RemoveBack(out int removedBack))
                            Console.WriteLine($"Removed: {removedBack}");
                        else
                            Console.WriteLine("Deque is empty!");
                        break;

                    case "5": // ВЫВЕСТИ ВЕСЬ ДЕК
                        deque.Print();  // Вызываем метод печати
                        break;

                    case "6": // ОЧИСТИТЬ ДЕК
                        deque.Clear();
                        Console.WriteLine("Deque cleared!");
                        break;

                    case "7": // РАЗДЕЛИТЬ НА ПОЛОЖИТЕЛЬНЫЕ И ОТРИЦАТЕЛЬНЫЕ
                        SplitDeque(deque);  // Вызываем функцию разделения
                        break;

                    case "8": // ВЫХОД ИЗ ПРОГРАММЫ
                        running = false;  // Меняем флаг - цикл завершится
                        break;

                    default:  // Если пользователь ввёл что-то другое (не 1-8)
                        Console.WriteLine("Invalid choice!");
                        break;
                }
                Console.WriteLine();  // Пустая строка для красоты
            }
        }

        /// ФУНКЦИЯ РАЗДЕЛЕНИЯ ДЕКА
        /// Создаёт два новых дека: один с положительными числами (>=0), другой с отрицательными (<0)
        /// </summary>
        /// <param name="source">Исходный дек, который нужно разделить</param>
        static void SplitDeque(Deque<int> source)
        {
            // Проверка: если исходный дек пуст - разделение невозможно
            if (source.IsEmpty)
            {
                Console.WriteLine("Deque is empty, cannot split!");
                return;
            }

            // Создаём два новых пустых дека
            Deque<int> positive = new Deque<int>();  // Для положительных чисел (>=0)
            Deque<int> negative = new Deque<int>();  // Для отрицательных чисел (<0)

            // Берём указатель на голову исходного дека
            DequeNode<int> current = source.Head;

            // Идём по всем узлам, пока не дойдём до конца
            while (current != null)
            {
                int num = current.Data;  // Берём число из узла

                if (num >= 0)
                    positive.AddBack(num);   // Положительное или ноль - в positive
                else
                    negative.AddBack(num);   // Отрицательное - в negative

                current = current.Next;  // Переходим к следующему узлу
            }

            // Выводим результат на экран
            positive.Print("Positive numbers (>=0)");
            negative.Print("Negative numbers (<0)");
        }
    }
}