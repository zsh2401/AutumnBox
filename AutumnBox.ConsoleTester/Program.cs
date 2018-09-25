using AutumnBox.Basic.Device;
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
            _xcount = 1;
            string exeAssembly = Assembly.GetEntryAssembly().FullName;
            // Construct and initialize settings for a second AppDomain.
            AppDomainSetup ads = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,

                DisallowBindingRedirects = false,
                DisallowCodeDownload = true,
                ConfigurationFile =
                AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            };

            // Create the second AppDomain.
            AppDomain ad2 = AppDomain.CreateDomain("AD #2", null, ads);

            // Create an instance of MarshalbyRefType in the second AppDomain. 
            // A proxy to the object is returned.
            AppDomainTest mbrt =
                (AppDomainTest)ad2.CreateInstanceAndUnwrap(
                    exeAssembly,
                    typeof(AppDomainTest).FullName
                );

            // Call a method on the object via the proxy, passing the 
            // default AppDomain's friendly name in as a parameter.
            Task.Run(()=> {
                mbrt.RunNoS();
            });
            Thread.Sleep(3000);
            // Unload the second AppDomain. This deletes its object and 
            // invalidates the proxy object.
            AppDomain.Unload(ad2);
            //try
            //{
            //    // Call the method again. Note that this time it fails 
            //    // because the second AppDomain was unloaded.
            //    mbrt.SomeMethod(callingDomainName);
            //    Console.WriteLine("Sucessful call.");
            //}
            //catch (AppDomainUnloadedException)
            //{
            //    Console.WriteLine("Failed call; this is expected.");
            //}
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
