using System;
using System.Threading.Tasks;
using System.Threading;

namespace task_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер массива случайных чисел: ");
            int a = 0;  //размер массива
            try
            {
                a = Convert.ToInt32(Console.ReadLine());
                int[] array = new int[a];   //создаем массив
                Random rnd = new Random();
                Console.WriteLine("Массив[{0}] случайных чисел:", a);
                for (int i = 0; i < a; i++) //заполняем случайными числами от 0 до 100
                {
                    array[i] = rnd.Next(100);
                    Console.WriteLine(array[i]);
                }
                Action<object> action = new Action<object>(SummArray);  //запускаем SummArray
                Task task1 = new Task(action, array);

                Action<Task, object> actionTask = new Action<Task, object>(MaxArray);   //запускаем MaxArray
                Task task2 = task1.ContinueWith(actionTask, array);
                task1.Start();
                Console.WriteLine("Метод Main закончил работу.");
            }
            catch
            {
                Console.WriteLine("Не удолось прочитать число.");
            }
            Console.ReadKey();
        }
        static void SummArray(object a) //расчет суммы членов
        {
            int[] arr = (int[])a;
            Console.WriteLine("Метод SummArray начал работу.");
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                result = result + arr[i];
                Thread.Sleep(200);
            }
            Console.WriteLine("Сумма членов массива равна: {0}", result);
            //return result;
            Console.WriteLine("Метод SummArray закончил работу.");
        }
        static void MaxArray(Task task, object a)   //поиск максимального
        {
            int[] arr = (int[])a;
            Console.WriteLine("Метод MaxArray начал работу.");
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (result <= arr[i])
                {
                    result = arr[i];
                    Thread.Sleep(100);
                }
            }
            Console.WriteLine("Максчимальный член массива равен: {0}", result);
            //return result;
            Console.WriteLine("Метод MaxArray закончил работу.");
        }
    }
}