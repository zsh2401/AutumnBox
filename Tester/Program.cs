using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using System;
using AutumnBox.Basic.Executer;

namespace Tester
{
    class Fuck : FunctionModule
    {
        //Base64FormattingOptions.
        protected override OutputData MainMethod()
        {
            return ae("get the fucker!");
        }
    }
    /// <summary>
    /// 测试
    /// </summary>
    class Program
    {
        static string mi4ID = "9dd1b490";
        static string mi6ID = "af0fe186";
        
        //[DllImport("kernel32.dll")]
        //static extern bool GenerateConsoleCtrlEvent(int dwCtrlEvent, int dwProcessGroupId);

        //[DllImport("kernel32.dll")]
        //static extern bool SetConsoleCtrlHandler(IntPtr handlerRoutine, bool add);

        //[DllImport("kernel32.dll")]
        //static extern bool AttachConsole(int dwProcessId);
        unsafe static void Test(int* fuck)
        {
            Console.WriteLine(*fuck);
        }
        static void Main(string[] args)
        {
            //DevicesMonitor monitor = new DevicesMonitor();
            //monitor.DevicesChange += (s, e) => { Print("Device Change"); e.ForEach((i)=> { Console.WriteLine(i.Id); }); };
            //monitor.Start();
            //DevicesHelper.GetDevices().ForEach((a)=> { Print("?asdad" + a.Id); });
            DeviceLink link = DeviceLink.Create();
            var rm = link.GetRunningManager(new Fuck());
            rm.FuncEvents.Finished+= (s, e) =>
            {
                Print(e.OutputData.Out.ToString());
                Print(e.OutputData.Error.ToString());
            };
            rm.FuncStart();
            
            //rm.FuncFinished += (s, e) => {
            //    Print(e.OutErrorData.Out.ToString());
            //    Print(e.OutErrorData.Error.ToString());
            //};
            //rm.FuncStart();
            //var a = new DeviceSimpleInfo { Id = mi4ID, Status = DeviceStatus.FASTBOOT };
            //var b = new DeviceSimpleInfo { Id = mi6ID, Status = DeviceStatus.RUNNING };
            //DevicesList old = new DevicesList() { a };
            //DevicesList _new = new DevicesList() { a,b,a,b,b,b};
            //Print((old != _new).ToString());
            //DeviceLink link = DeviceLink.Create();

            //Print(link.Info.Status.ToString());
            //var x = new FileSender(new FileArgs() { files = new string[] { "E:/MiDreamOut.zip" } });
            //var rm = link.InitRM(x);
            //rm.FuncStarted += (s, e) => { Console.WriteLine("Start!"); };
            //rm.FuncFinished += (s, e) => { Console.WriteLine("Finish"); };
            //rm.OutputReceived += (s, e) => { Console.WriteLine(e.Data); };
            //rm.ErrorReceived += (s, e) => { Console.WriteLine(e.Data); };
            //rm.ExecuterStared += (s, e) => { Console.WriteLine(e.PID); };
            //Console.ReadKey();
            //rm.FuncStart();
            //DevicesMonitor dl = new DevicesMonitor();
            ////var o = new List<string>() + new List<string>();
            //dl.DevicesChange += (obj, list) => { Print("Devices Change"); list.ForEach((info) => { Print(info.Id); }); };
            //dl.Start();
            //DevicesHelper.GetBuildInfo(mi6ID);
            //var o = DevicesHelper.GetDevices();
            //o.ForEach((a)=> { Console.WriteLine(a); });
            //List<string> ls = new List<string>();
            //List<string> sb = new List<string>();
            //ls.Add("sasdas");
            //sb.Add("sasdas");
            //bool q = ls == sb
            //Print(q.ToString());
            Console.ReadKey();
            //rm.FuncStop();
            //string x = " [30%] wadfsadasasasasd";
            //string x = "[ 98%] /sdcard/D:\\xxxxx_x64.zip";
            //var rex = new Regex("[(?<MYSTR>\\w+)]");
            //String str1 = rex.Match(x).Groups["MYSTR"].ToString();
            //Regex rg = new Regex("(?<=(" + "[" + "))[.\\s\\S]*?(?=(" + "]" + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            //Regex rg = new Regex(@"\(([^)]*)\)");
            //Regex rg = new Regex("\\ (.*?)\\%");
            //var r = rg.Match(x).Result("$1");
            //CommandExecuter executer = new CommandExecuter();
            //DevicesHashtable dh = new DevicesHashtable();
            ////executer.GetDevices(ref dh);
            //var o = executer.Execute("reboot ");
            //Print(o.Out.ToString());
            //foreach (DictionaryEntry x in dh) {
            //    Print(x.Key.ToString());
            //}
            //string result = rg.Match(x).Result("$1").Remove(0, 2).Remove(r.Length - 3,1);
            //Print(r);
            //var rex = new Regex("（(?<MYSTR>\\w+)）");
            //String str1 = rex.Match("aaaa（bbbbbb）jlkoihj").Groups["MYSTR"].ToString();
            //Console.WriteLine(str1);
            //var x = new FileSender(new FileArgs() { files = new string[] { "E:/MiDreamOut.zip" } });
            //DeviceLink link = DeviceLink.Create(mi6ID);
            //var rm = link.InitRM(x);
            //rm.FuncStarted += (s, e) => { Console.WriteLine("Start!"); };
            //rm.FuncFinished += (s, e) => { Console.WriteLine("Finish"); };
            //rm.OutputReceived += (s, e) => { Console.WriteLine(e.Data); };
            //rm.ErrorReceived += (s, e) => { Console.WriteLine(e.Data); };
            //rm.ExecuterStared += (s, e) => { Console.WriteLine(e.PID); };
            ////Console.WriteLine(rm)
            //rm.FuncStart();
            //Console.ReadKey();
            //rm.FuncStop();
            //x.
            //DevicesListener l = new DevicesListener();
            //l.DevicesChange += (s, h) => { Console.WriteLine("Device Change"); };
            //l.Start();

            //DevicesHelper.GetBuildInfo(mi6ID);
            //DeviceLink link;
            //foreach (DictionaryEntry e in DevicesHelper.GetDevices())
            //{
            //    link = DeviceLink.Create(e);
            //    break;
            //}
            //link


            //var deviceList = DevicesHelper.GetDevices();
            //Console.WriteLine("设备列表");
            //foreach (DictionaryEntry i in deviceList) {
            //    Console.WriteLine(i.ToString());
            //}
            //Console.ReadKey();
            //FileSender fs = new FileSender(new FileArgs() { files = new string[] { "E:/MiDreamOut.zip" } });

            //var rm = link.Execute(fs);
            //rm.FuncStop();
            //DevicesListener listener = new DevicesListener();
            //listener.DevicesChange += (s, e) => { Console.WriteLine("Devices Change"); };
            //listener.Start();
            //Adb adb = new Adb();
            //var t = new Thread(() => { adb.Execute("push E:/MiDreamOut.zip /sdcard/"); });
            //t.Start();
            //Console.ReadKey();
            //Console.WriteLine("Clear");
            //adb.Stop();
            //Console.WriteLine(p.Threads[0].Id);

            //adb.cmdProcess.input
            //var sw = adb.cmdProcess.StandardInput;
            //adb.cmdProcess.k
            //sw.WriteLine("{Ctrl}+{C}");
            //adb.cmdProcess.Kill();
            //DevicesTools.GetDevices();

            //DeviceLink link = DeviceLink.Create();
            //var fs = new FileSender(new FileArgs { files = new string[] { @"D:\☆下载暂存\RiseCraft第三大道定制(1).7z.baiduyun.downloading" } });
            //fs.Finish += (s,e)=>  { Console.WriteLine("Finish"); };
            //var ba = new BreventServiceActivator();
            //ba.Finish += (s, e) => { Console.WriteLine("Finish"); };
            //var rm =  link.Execute(fs);
            //Console.ReadKey();
            //Console.WriteLine(1);
            //listener.Stop();
            //Console.WriteLine(1);
            //Adb.RestartAdb();
            //listener.Start();
            //rm.Stop();
            //Console.ReadKey();
            //Cmd cmd = new Cmd();
            //cmd.OutputDataReceived += (s, e) => { Console.WriteLine(e.Data); };
            //var t= new Thread(() => { cmd.Execute("ping www.baidu.com"); });
            //t.Start();
            //Thread.Sleep(2000);
            ////t.Abort();
            //cmd.cmdProcess.Kill();
            //Console.WriteLine(cmd.cmdProcess.HasExited);
            //Console.ReadKey();
            //var fm = new BreventServiceActivator();
            //RunningManager rm;
            //fm.ErrorDataReceived += (s, e) => { Console.WriteLine(e.Data); };
            //fm.OutputDataReceived += (s, e) => { Console.WriteLine(e.Data); };
            //fm.Finish += (s, e) => { Console.WriteLine("Finish"); };
            //rm = DeviceLink.Create().Execute(fm);
            //Thread.Sleep(2000);
            //rm.Stop();
            //Console.WriteLine(rm.FunctionIsFinish);
            //Console.ReadKey();
            //var fm = new RebootOperator(new RebootArgs {
            //    nowStatus = DevicesTools.GetDeviceStatus(link.DeviceID),rebootOption = RebootOptions.System}
            //);
            //var fm = new ApplicationLauncher(new ApplicationLaunchArgs { PackageName = "me.piebridge.brevent", ActivityName = ".ui.BreventActivity" });
            //fm.Finish += (s, a) => { Console.WriteLine(a.OutputData.nOutPut); };
            ////( fm as ICanGetRealTimeOut).OutputDataReceived += (s, o) => { Console.WriteLine(o.Data); };
            //link.Execute(fm);
            //Console.ReadKey();
            //foreach (DictionaryEntry i in DevicesTools.GetDevices()) {
            //    Console.WriteLine(i.Key.ToString());
            //}
            //Adb adb = new Adb();
            //var d = adb.GetDevices();
            //foreach (DictionaryEntry i in d) {
            //    Console.WriteLine(i.Key);
            //}
            //Process cmdProcess = new Process();
            //cmdProcess.StartInfo.FileName = "cmd.exe";
            //cmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            //cmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            //cmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            //cmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            //cmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  

            //cmdProcess.StartInfo.Arguments = @"/c ping www.baidu.com";
            //cmdProcess.Start();
            //cmdProcess.BeginOutputReadLine();
            //cmdProcess.BeginErrorReadLine();
            //cmdProcess.OutputDataReceived += (s, d) => { Console.WriteLine(d.Data); };
            //cmdProcess.ErrorDataReceived += (s, d) => { Console.WriteLine(d.Data); };
            //cmdProcess.WaitForExit();
            //cmdProcess.Close();
            //link.Execute(fm);
            //new BreventServiceActivator().DeviceID
            //new Adb().Execute("device");
            //Device d = Device.GetDeviceFromID(mi4ID);
            //var rf = new XiaomiBootloaderRelocker();
            //var rf = new XiaomiSystemUnlocker();
            //rf.DeviceID = mi4ID;
            //Console.WriteLine(d.Info.deviceStatus);
            //rf.Args = new RebootArgs { deviceID = mi4ID, nowStatus = d.Info.deviceStatus, rebootOption = RebootOptions.System };
            //d.Execute(rf);
            //Console.WriteLine(new AutumnBox.Basic.AdbEnc.Adb().Execute("help").output[0]);
            //Adb adb = new Adb();
            //var x = adb.GetDevices();
            //foreach (DictionaryEntry i in x) {
            //    Console.WriteLine(i.Key);
            //}

            //rf.
            //rf.Finish += (s, e) => { Console.WriteLine("Ok"); };
            //rf.Args = new Args { deviceID = mi4ID };
            //rf.Args = new FileArgs {deviceID = mi4ID, files = new string[] { @"E:\VSProject\CSharp\AutumnKMCCC\KMCCC.sln" } };
            //rf.Args = new RebootArgs { deviceID = mi4ID, nowStatus = DevicesTools.GetDeviceStatus(mi4ID), rebootOption = RebootOptions.System };



            //Console.WriteLine(DevicesTools.GetDeviceStatus(mi4ID));
            //devices

            //d.Execute
            //AutumnBox.Basic.
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
            //try
            //{
            //    Print(Tools.GetHtmlCode("https://raw.githubusercontent.com/zsh2401/AutumnBox/master/Api/update2401.jsonss"));
            //}
            //catch { }
            //AutumnBox.Basic.DebugTools.Tester t = new AutumnBox.Basic.DebugTools.Tester();

            //AutumnBox.Basic.DebugTools.Tester.Reboot("9dd1b490", RebootOptions.System);

            //Print(DevicesTools.GetDeviceStatus(mi4ID).ToString());
            //Print(DevicesTools.GetBuildInfo(mi4ID)["name"].ToString());
            //DevicesHashtable devices = DevicesTools.GetDevices();
            //foreach (DictionaryEntry i in devices) {
            //    Print(i.Key.ToString() + i.Value.ToString());
            //}
            //Print(DevicesTools.GetDeviceInfo(mi4ID).androidVersion);
            //AutumnBox.Basic.StaticFunctions.DevicesTools

            //List<DeviceInfo> l = DevicesTools.GetDevicesInfo();
            //foreach (DeviceInfo i in l) {
            //    Print(i.brand + i.model);
            //}
            //AutumnBox.Basic.DebugTools.Tester.SendFileToSdcard(mi4ID, new string[] { @"E:\AutumnBox\bin\AutumnBox.exe" });
            //AutumnBox.Basic.DebugTools.Tester.Reboot(mi4ID,RebootOptions.Bootloader);
            //AutumnBox.Basic.DebugTools.Tester.FlashCustomRecovery(mi4ID, @"D:\☆下载暂存\twrp-3.0.2-0-cancro-launguagefix.img");
            //AutumnBox.Basic.DebugTools.Tester.Reboot(mi4ID,RebootOptions.System);
            //Print(new AutumnBox.Util.NoticeGetter().GetNotice().content);
            //Print(new AutumnBox.Util.UpdateChecker().GetUpdateInfo().content);
            //UpdateChecker checker = new UpdateChecker();
            //checker.UpdateCheckFinish += new UpdateChecker.UpdateCheckFinishHandler((value,info) => {
            //    Print(info.content);
            //});
            //checker.Check();
            //NoticeGetter getter = new NoticeGetter();
            //getter.NoticeGetFinish += new NoticeGetter.NoticeGetFinishHandler((notice)=> {
            //    Print(notice.content);
            //});
            //getter.Get();
            //Console.ReadKey();
            //Print(Tools.GetNotice());
            //Version v1 = new Version("0.12.8.11231");
            //Version v2 = new Version("1.0.1");
            //Print((v1 < v2).ToString());
            //Print((Environment.OSVersion.Version.Major == 10).ToString());
            //Print(Environment.OSVersion.Version.Major.ToString());
        }

        public static void Print(string str)
        {
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
