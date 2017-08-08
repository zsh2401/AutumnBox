using System;

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
            ConfigSql t = new ConfigSql();
            t.Set("intValues", "test",null);
            Console.WriteLine(t.Read("intValues","test"));
            //t.Set("boolValues","test",false);
            //Console.WriteLine(t.Read("boolValues", "test"));
            //SQLiteTest sqlTest = new SQLiteTest();
        }
        //[DllImport("AdbWinApi.dll", CallingConvention = CallingConvention.Winapi)]
        //public extern static int AdbGetUsbDeviceDescriptor();

        //[DllImport("AdbWinApi.dll", CallingConvention = CallingConvention.Winapi)]
        //public extern static int AdbGetSerialNumber();

        //[DllImport("AdbWinApi.dll", CallingConvention = CallingConvention.Winapi)]
        //public extern static int AdbGetInterfaceName();
    }
}
