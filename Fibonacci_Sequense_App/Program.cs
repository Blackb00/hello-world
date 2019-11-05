using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fibonacci_Sequense_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("If you want to start fibonacci sequence, please push enter. If you want to pause the sequence, push the space. If you want to quit, push escape.");
            Console.ReadLine();
            ManualResetEvent idleAcceptedEvent = new ManualResetEvent(true);

            Fibonacci fibo = new Fibonacci();
            new Action(() => {
                while (true)
                {
                    foreach (decimal i in fibo.Go())
                    {
                        idleAcceptedEvent.WaitOne();
                        Console.WriteLine(i);
                        Thread.Sleep(100);
                    };
                }

            }).BeginInvoke(null, null);

            bool active = false;
            for (; ; )
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Spacebar)
                    if (active = !active)
                        idleAcceptedEvent.Reset();
                    else idleAcceptedEvent.Set();
                else if (info.Key == ConsoleKey.Escape)
                    return;
            }
        }
    }

    class Fibonacci
    {
        public IEnumerable Go()
        {
            decimal temp;
            decimal firstNum = 1;
            decimal secondNum = 2;

            for (int i = 1; ; i++)
            {
                if ((decimal)i == 1 || (decimal)i == 2)
                {
                    temp = i;
                }
                else
                {
                    try
                    {
                        temp = firstNum + secondNum;
                        firstNum = secondNum;
                        secondNum = temp;
                    }
                    catch
                    {
                        yield break;
                    }

                }

                yield return temp;
            }
        }
    }
}
