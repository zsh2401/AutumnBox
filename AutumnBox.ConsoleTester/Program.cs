using System;
using System.Collections;

namespace AutumnBox.ConsoleTester
{
    class Program 
    {
        private void Run()
        {
          object value =   new Hashtable() {
                { "a","b"}
            }["b"];
            Console.WriteLine(value == null);
        }
        enum A {
            a = 1 << 0,
            b = 1 << 1,
            c = 1 << 2,
            d = 1 << 3
        }
            
        unsafe static int Main(string[] cmdargs)
        {
            var a = A.a | A.b;
            var b = A.d | A.c;
            Console.WriteLine((a & b) == 0);
            Console.ReadKey();
            return 0;
        }
    }
}
