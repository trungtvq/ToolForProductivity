using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace RarMining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //RarFile test =new RarFile(@"E:\", "bbb.rar",true, 8, "123456789zxcvbnmasdfghjklqwertyuiop");
            //test.MulithreadExtract(1);
            RarFile test=new RarFile(@"E:\", "bbb.rar","321654987");
            test.DoExtract();
        }

        
        
        
    }
    
    
}
