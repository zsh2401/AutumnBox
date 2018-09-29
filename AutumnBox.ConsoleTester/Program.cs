using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.MultipleDevices;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester
{
    public static class Extension
    {
        public static AdbResponse Print(this AdbResponse response)
        {
            Console.Write(response.State + ":");
            try
            {
                Console.Write(response.DataAsString());
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
            }
            Console.Write("\n");
            return response;
        }
    }
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
       static int _xcount = 0;
        unsafe static void Main(string[] cmdargs)
        {
            var intent  = new Intent();
            intent.Add("z",3);
            intent.Add("h",true);
            intent.Add("wtf","asdas");
            intent.Add("wow",new int[] { 1, 2, 3 });
            Console.WriteLine(IntentStringHelper.ToAdbArguments(intent));
        }
        [Serializable]
        private class AppDomainTest
        {
            public void RunNoS() {
                int count = 0;
                while (true)
                {
                    count++;
                    Console.WriteLine(count);
                    Thread.Sleep(1000);
                }
            }
            public void Run()
            {
                Task.Run(() =>
                {
                    int count = 0;
                    while (true)
                    {
                        count++;
                        Console.WriteLine(count);
                        Thread.Sleep(1000);
                    }
                });
            }
            ~AppDomainTest() {
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine("no!!!");
            }
        }
        private static byte[] BuildCommand(string command)
        {
            string resultStr = string.Format("{0}{1}\n", command.Length.ToString("X4"), command);
            byte[] result = Encoding.UTF8.GetBytes(resultStr);
            return result;
        }
    }
}
