using System;

namespace Lab4_3_SavinDA
{
    class DequeNode<T>
    {
        public T Data { get; set; }
        public DequeNode<T> Next { get; set; }
        public DequeNode<T> Previous { get; set; }

        public DequeNode(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }

    class Deque<T>
    {
        private DequeNode<T> head;
        private DequeNode<T> tail;
        private int count;

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        public DequeNode<T> Head { get { return head; } }

        public void AddFront(T data)
        {
            DequeNode<T> node = new DequeNode<T>(data);

            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                node.Next = head;
                head.Previous = node;
                head = node;
            }
            count++;
        }

        public void AddBack(T data)
        {
            DequeNode<T> node = new DequeNode<T>(data);

            if (tail == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                tail.Next = node;
                node.Previous = tail;
                tail = node;
            }
            count++;
        }

        public bool RemoveFront(out T value)
        {
            value = default(T);

            if (head == null)
                return false;

            value = head.Data;

            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.Previous = null;
            }

            count--;
            return true;
        }

        public bool RemoveBack(out T value)
        {
            value = default(T);

            if (tail == null)
                return false;

            value = tail.Data;

            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                tail = tail.Previous;
                tail.Next = null;
            }

            count--;
            return true;
        }

        public bool PeekFront(out T value)
        {
            value = default(T);
            if (head == null) return false;
            value = head.Data;
            return true;
        }

        public bool PeekBack(out T value)
        {
            value = default(T);
            if (tail == null) return false;
            value = tail.Data;
            return true;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Print(string name = "Deque")
        {
            Console.Write($"{name}: ");

            if (head == null)
            {
                Console.WriteLine("empty");
                return;
            }

            DequeNode<T> current = head;
            while (current != null)
            {
                Console.Write($"{current.Data} ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Deque<int> deque = new Deque<int>();
            bool running = true;

            Console.WriteLine("=== DEQUE VARIANT 3 ===");
            Console.WriteLine("1.Add front  2.Add back   3.Remove front  4.Remove back");
            Console.WriteLine("5.Print      6.Clear      7.Split +/-     8.Exit\n");

            while (running)
            {
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter number: ");
                        if (int.TryParse(Console.ReadLine(), out int f))
                            deque.AddFront(f);
                        break;

                    case "2":
                        Console.Write("Enter number: ");
                        if (int.TryParse(Console.ReadLine(), out int b))
                            deque.AddBack(b);
                        break;

                    case "3":
                        if (deque.RemoveFront(out int removedFront))
                            Console.WriteLine($"Removed: {removedFront}");
                        else
                            Console.WriteLine("Deque is empty!");
                        break;

                    case "4":
                        if (deque.RemoveBack(out int removedBack))
                            Console.WriteLine($"Removed: {removedBack}");
                        else
                            Console.WriteLine("Deque is empty!");
                        break;

                    case "5":
                        deque.Print();
                        break;

                    case "6":
                        deque.Clear();
                        Console.WriteLine("Deque cleared!");
                        break;

                    case "7":
                        SplitDeque(deque);
                        break;

                    case "8":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void SplitDeque(Deque<int> source)
        {
            if (source.IsEmpty)
            {
                Console.WriteLine("Deque is empty, cannot split!");
                return;
            }

            Deque<int> positive = new Deque<int>();
            Deque<int> negative = new Deque<int>();

            DequeNode<int> current = source.Head;

            while (current != null)
            {
                int num = current.Data;

                if (num >= 0)
                    positive.AddBack(num);
                else
                    negative.AddBack(num);

                current = current.Next;
            }

            positive.Print("Positive numbers (>=0)");
            negative.Print("Negative numbers (<0)");
        }
    }
}
