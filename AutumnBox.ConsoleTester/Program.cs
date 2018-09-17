using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.MultipleDevices;
using System;
using System.Collections;
using System.Text;

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
        unsafe static void Main(string[] cmdargs)
        {
            var input = "asdjkasdsalk fastboot -afskl\"dasjajlk";
            if (DeviceObjectFacotry.AdbTryParse(input, out IDevice device))
            {
                Console.WriteLine(device.SerialNumber);
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
