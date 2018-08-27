using AutumnBox.MapleLeaf.Adb;
using AutumnBox.MapleLeaf.Android;
using AutumnBox.MapleLeaf.Android.Shell;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
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
        unsafe static void Main(string[] cmdargs)
        {
            using (IAdbService adbService = AdbService.Instance)
            {
                adbService.Start(5099);
                AndroidShell shell = new AndroidShell("9dd1b490");
                shell.OutputReceived += (s, e) =>
                {
                    Console.Write(e.Content);
                };
                InputLoop(shell);
            }
            Console.ReadKey();
            //byte[] headBuffer = new byte[sizeof(byte) + sizeof(Int32)];
            //byte[] buffer = new byte[1];
            //using (AdvanceAdbClient client = new AdvanceAdbClient())
            //{
            //    client.Connect();
            //    client.SetDevice("9dd1b490");
            //    client.SendRequestNoData("shell:");
            //    client.Socket.Receive(headBuffer);
            //    //Console.WriteLine("header buffer");
            //    //foreach (byte b in headBuffer)
            //    //{
            //    //    Console.Write(headBuffer[1]);
            //    //}
            //    //Console.WriteLine("header buffer");
            //    using (Stream stream = client.GetStream())
            //    {
            //        while (stream.CanRead)
            //        {
            //            stream.Read(buffer, 0, buffer.Length);
            //            //Console.Write(Convert.ToString(buffer[0], 16) + " ");
            //            //if (buffer[0] == 0xa && stream.Length) break;
            //            Console.Write(Encoding.UTF8.GetString(buffer));
            //        }
            //    }
            //    Console.Write("\n");
            //}
            //foreach (byte b in buffer)
            //{
            //    Console.Write(Convert.ToString(b, 16) + " ");
            //}
        }
        private static void InputLoop(IAndroidShell shell)
        {
            while (true)
            {
                shell.InputLine(Console.ReadLine());
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
