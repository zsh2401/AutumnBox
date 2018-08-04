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
        unsafe static int Main(string[] cmdargs)
        {
            object value = new Hashtable() {
                { "a","b"}
            }["b"];
            Console.WriteLine(value == null);
            Console.ReadKey();
            return 0;
        }
    }
}
