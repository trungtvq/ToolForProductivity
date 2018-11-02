using System;
using System.Diagnostics;
using System.Timers;
using Timer = System.Threading.Timer;

namespace CheckPortOpen
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch=new Stopwatch();
            stopWatch.Restart();
            SupportMultiThreadingCheckPort test = new SupportMultiThreadingCheckPort();
            test.CheckPort("trungtvq.ddns.net",1, 1000,144);
            Console.WriteLine("Elapsed time {0} ms", stopWatch.ElapsedMilliseconds);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
