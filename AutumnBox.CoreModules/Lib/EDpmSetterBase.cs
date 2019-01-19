/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Exceptions;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.CoreModules.Lib
{
    [ExtDesc("使用奇淫技巧暴力设置设备管理员,\n注意:使用此模块前,必须先移除屏幕锁,指纹锁等,否则将可能导致不可预见的后果", "en-us:Use the sneaky skills to set up the device administrator, \n Note: Before using this module, you must first remove the screen lock, fingerprint lock, etc., otherwise it may lead to unforeseen consequences")]

    [ExtIcon("Icons.nuclear.png")]
    [DpmReceiver(null)]
    [ExtText("Warning", 
        "This module will violently delete your account and users, and set this app as the device owner. Are you sure you want to do this?",
        "zh-cn:本模块将会暴力删除你的账户与用户,并设置这个应用为设备管理员(免ROOT),你确定这么做吗?")]
    [ExtText("WarningRemoveLock",
        "there is only one thing you need to do:\ndelete screen lock and fingerprint lock before next step\ndelete screen lock and fingerprint lock before next step", 
        "zh-cn:你只需要做一件事!完成后再点击继续!\n删除屏幕锁和指纹锁\n删除屏幕锁和指纹锁\n删除屏幕锁和指纹锁")]
    [ExtText("WarningLastChance", 
        "This is the last chance to cancel,are sure to continue?", 
        "zh-cn:这是你最后一次取消的机会,一旦开始操作将不可停止")]
    [ExtText("Extract", "Extract dpmpro to device", "zh-cn:提取dpmpro")]
    [ExtText("Push", "Pushing dpmpro to device", "zh-cn:推送dpmpro")]
    [ExtText("RMAcc", "Removing accounts", "zh-cn:正在移除所有账号")]
    [ExtText("RMUser", "Removing users", "zh-cn:正在移除所有用户")]
    [ExtText("SettingDpm", "Setting device owner", "zh-cn:正在设置设备管理员")]
    [ExtText("UseDpmPro", "The dpm command not found,do you wanna try dpmpro?", "zh-cn:您的设备缺少DPM,使用DPMPRO吗?")]
    [ExtText("MaySuccess", "May successed", "zh-cn:可能成功")]
    [ExtText("MaySuccessAdvice", "Maybe successful?Please check your app", "zh-cn:由于安卓DPM原因, 无法确定是否成功, 请自行查看手机进行判断")]
    internal abstract class EDpmSetterBase : LeafExtensionBase
    {
        /// <summary>
        /// UI
        /// </summary>
        [LProperty]
        public ILeafUI UI { get; set; }

        /// <summary>
        /// 日志器
        /// </summary>
        [LProperty]
        public ILogger Logger { get; set; }

        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="texts"></param>
        /// <param name="ui"></param>
        /// <param name="context"></param>
        [LMain]
        public void Main(IDevice device, TextAttrManager texts)
        {
            using (UI)
            {
                //初始化UI
                InitUI();

                //显示UI
                UI.Show();

                //第一次警告
                if (!UI.DoYN(texts["Warning"]))
                {
                    UI.Shutdown();
                    return;
                }
                //提示用户移除屏幕锁等
                if (!UI.DoYN(texts["WarningRemoveLock"]))
                {
                    UI.Shutdown();
                    return;
                }
                //最后一次机会
                if (!UI.DoYN(texts["WarningLastChance"]))
                {
                    UI.Shutdown();
                    return;
                }

                //获取要设置的接收器组件名
                string componentName = GetComponentName();

                //构造一个命令执行器
                using (CommandExecutor executor = new CommandExecutor())
                {
                    //将命令执行器输出定向到界面
                    executor.To(e => UI.WriteOutput(e.Text));

                    //构造一个dpmpro的控制器
                    var dpmpro = new CstmDpmCommander(executor, new DpmContext(), device);

                    //将dpmpro提取到临时目录
                    UI.Progress = 0;
                    UI.Tip = texts["Extract"];
                    UI.WriteLine(texts["Extract"]);
                    dpmpro.Extract();


                    //推送dpmpro到设备
                    UI.Progress = 20;
                    UI.Tip = texts["Push"];
                    UI.WriteLine(texts["Push"]);
                    dpmpro.PushToDevice();

                    //移除账户
                    UI.Progress = 40;
                    UI.Tip = texts["RMAcc"];
                    UI.WriteLine(texts["RMAcc"]);
                    dpmpro.RemoveAccounts();

                    //移除用户
                    UI.Progress = 60;
                    UI.Tip = texts["RMUser"];
                    UI.WriteLine(texts["RMUser"]);
                    dpmpro.RemoveUsers();

                    //使用设备自带dpm设置管理员,并记录结果
                    UI.Progress = 80;
                    UI.Tip = texts["SettingDpm"];
                    UI.WriteLine(texts["SettingDpm"]);
                    var resultOfSetDpm = executor.AdbShell(device, "dpm set-device-owner", componentName);

                    //如果返回值为127,也就是说这设备连dpm都阉割了,就询问用户是否用dpmpro来设置设备管理员
                    if (resultOfSetDpm.ExitCode == 127 && UI.DoYN(texts["UseDpmPro"]))
                    {
                        //用dpmpro设置设备管理员,并记录结果
                        resultOfSetDpm = dpmpro.SetDeviceOwner(componentName);
                    }
                    //使用输出解析器,对记录的输出进行解析
                    if (DpmFailedMessageParser.TryParse(resultOfSetDpm, out string tip, out string message))
                    {
                        //解析成功,在输出框写下简要信息与建议
                        UI.WriteLine(message);
                        //UI流程结束
                        UI.Finish(tip);
                    }
                    else
                    {
                        //解析失败,告诉用户可能成功
                        UI.ShowMessage(texts["MaySuccessAdvice"]);
                        UI.WriteLine(texts["MaySuccessAdvice"]);
                        UI.Finish(texts["MaySuccess"]);
                    }
                }
            }
        }

        /// <summary>
        ///  初始化UI
        /// </summary>
        private void InitUI()
        {
            UI.Icon = this.GetIconBytes();
            UI.Title = this.GetName();
            var viewSize = UI.Size;
            viewSize.Height += 100;
            viewSize.Width += 100;
            UI.Size = viewSize;

            //UI.EnableHelpBtn(() =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Process.Start("http://www.atmb.top/go/help/dpmhelp");
            //    }
            //    catch (Exception e)
            //    {
            //        Logger.Warn("cannot go to dpm setter's help", e);
            //    }
            //});
        }

        /// <summary>
        /// 获取子类需要设置的DPM接收器组件名
        /// </summary>
        /// <returns></returns>
        public string GetComponentName()
        {
            ClassExtensionScanner scanner = new ClassExtensionScanner(GetType());
            scanner.Scan(ClassExtensionScanner.ScanOption.Informations);
            var infos = scanner.Informations;
            return infos[DpmReceiverAttribute.KEY].Value as string;
        }

        /// <summary>
        /// 用来确定临时目录位置
        /// </summary>
        public class DpmContext : Context { }
    }
}
