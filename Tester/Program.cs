using AutumnBox.Basic;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
namespace Tester
{
    /// <summary>
    /// 测试
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            ////测试设备列表获取
            //AdbTools at = new AdbTools();
            //DevicesHashtable normal_devices = at.GetDevice();
            //FastbootTools ft = new FastbootTools();
            //DevicesHashtable bootloader_devices = ft.GetDevice();
            //DevicesHashtable result = normal_devices + bootloader_devices;
            //foreach (DictionaryEntry i in result) {
            //    Console.WriteLine($"Status -> {i.Value}  ID{i.Key}");
            //}
            //Console.ReadKey();
            ////测试重载运算符
            //Console.WriteLine((at.GetDevice() + ft.GetDevice()) == result);
            //Core core = new Core();
            //core.devicesListener.DevicesChange += new DevicesListener.DevicesChangeHandler((obj, hs) =>
            //{
            //    Console.WriteLine("Devices Changed!!!!");
            //    foreach (DictionaryEntry de in hs)
            //    {
            //        Console.WriteLine($"Status -> {de.Value}  ID{de.Key}");
            //    }
            //});
            //DeviceInfo info = core.GetDeviceInfo("9dd1b490");
            //Console.WriteLine(info.deviceStatus);
            //core.Reboot("9dd1b490");
            //core.devicesListener.Start();
            //Console.ReadKey();
            //string[] x = { "9dd1b490", @"E:\VSProject\CSharp\MiDreamer\MDCore\Core.Constructor.cs" };
            //core.PushFileToSdcard(x);
            //core.Reboot("9dd1b490",RebootOptions.Bootloader);
            //string[] x = { "9dd1b490", @"D:\☆下载暂存\twrp-3.0.2-0-cancro-launguagefix.img" };
            //core.FlashCustomRecovery(x);
            //Console.WriteLine(AdbGetInterfaceName());
            //Console.ReadKey();
            //ConfigSql t = new ConfigSql();
            //t.Set("intValues", "test",null);
            //Console.WriteLine(t.Read("intValues","test"));
            //t.Set("boolValues","test",false);
            //Console.WriteLine(t.Read("boolValues", "test"));
            //SQLiteTest sqlTest = new SQLiteTest();
            //Core core = new Core();
            //core.Sideload(new string[] { "9dd1b490", @"D:\☆下载暂存\aicp_cancro_n-12.1-NIGHTLY-20170812.zip" });
            //Process.Start("t.bat", @"9dd1b490 D:\☆下载暂存\aicp_cancro_n-12.1-NIGHTLY-20170812.zip");
            //ProcessStartInfo i = new ProcessStartInfo();
            ////i.WorkingDirectory = @"adb\";
            //i.Arguments = @"9dd1b490 D:\☆下载暂存\aicp_cancro_n-12.1-NIGHTLY-20170812.zip";
            //i.FileName = @"util\t.bat";
            //Process.Start(i);
            //Process p = new Process();
            //p.StartInfo.Arguments = "dir";
            //p.StartInfo.FileName = @"util\t2.bat";
            //p.StartInfo.CreateNoWindow = true;
            //p.Exited += new EventHandler(P_Exited);
            //p.Start();
            //Core c = new Core();
            //c.Sideload(new string[] { "9dd1b490", @"D:\☆下载暂存\aicp_cancro_n-12.1-NIGHTLY-20170812.zip" });
            //Console.WriteLine(Tools.GetHtmlCode("http://www.baidu.com"));
            try
            {
                Print(Tools.GetHtmlCode("https://raw.githubusercontent.com/zsh2401/AutumnBox/master/Api/update2401.jsonss"));
            }
            catch { }
            
            //Print(Tools.GetNotice());
            //Version v1 = new Version("0.12.8.11231");
            //Version v2 = new Version("1.0.1");
            //Print((v1 < v2).ToString());
        }

        public static void Print(string str) {
            Console.WriteLine(str);
        }
        //private static void P_Exited(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Ok!");
        //    Debug.WriteLine("OK");
        //}
        //[DllImport("AdbWinApi.dll", CallingConvention = CallingConvention.Winapi)]
        //public extern static int AdbGetUsbDeviceDescriptor();

        //[DllImport("AdbWinApi.dll", CallingConvention = CallingConvention.Winapi)]
        //public extern static int AdbGetSerialNumber();

        //[DllImport("AdbWinApi.dll", CallingConvention = CallingConvention.Winapi)]
        //public extern static int AdbGetInterfaceName();
    }
}
