using System;
using System.Collections.Generic;
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

            List<int> t=test.CheckPort("bkpay.hcmut.edu.vn", 0, 60000,200);
            Console.WriteLine(t.Count);
            foreach (int i in t)
            {
                
                Console.WriteLine("Open at:"+ i.ToString());
            }
            Console.WriteLine("Elapsed time {0} ms", stopWatch.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
