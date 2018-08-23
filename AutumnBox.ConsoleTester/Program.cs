using System;
using System.Collections;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        private void Run()
        {
            object value = new Hashtable() {
                { "a","b"}
            }["b"];
            Console.WriteLine(value == null);
        }
        enum A
        {
            a = 1 << 0,
            b = 1 << 1,
            c = 1 << 2,
            d = 1 << 3
        }
        public static System.Threading.Tasks.Task<int> FuckAsync(System.Threading.CancellationToken token)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                return Fuck();
            }, token);
        }
        public static int Fuck()
        {
            System.Threading.Thread.Sleep(5 * 1000);
            Console.WriteLine("WOW!");
            return 1;
        }
        unsafe static int Main(string[] cmdargs)
        {
            var source = new System.Threading.CancellationTokenSource();
            Console.WriteLine("Run async");
            FuckAsync(source.Token);
            Console.WriteLine("?");
            Console.ReadKey();
            source.Cancel();
            Console.WriteLine("Canceled");
            Console.ReadKey();
            return 0;
        }
    }
}
