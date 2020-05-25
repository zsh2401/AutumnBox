using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;
using System.Threading;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using System;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    /// <summary>
    /// LeafUI相关拓展函数
    /// </summary>
    [ClassText("msg", "Do you have install the relative app?", "zh-cn:你似乎没有安装对应APP?")]
    [ClassText("continue", "Continue forcely", "zh-cn:强行继续")]
    [ClassText("ok", "Ok", "zh-cn:好")]
    [ClassText("cancel", "Cancel", "zh-cn:取消")]
    public static class LeafUIExtension
    {

        /// <summary>
        /// Leaf拓展被终结的信号
        /// </summary>
        internal class LeafExtensionTerminatedException : Exception { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="exitCode"></param>
        public static void EFinish(this ILeafUI ui, int exitCode = 0)
        {
            ui.Finish(exitCode.ToString());
            throw new LeafExtensionTerminatedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="tip"></param>
        public static void EFinish(this ILeafUI ui, string tip)
        {
            ui.Finish(tip);
            throw new LeafExtensionTerminatedException();
        }

        /// <summary>
        /// Shutdown
        /// </summary>
        /// <param name="ui"></param>
        public static void EShutdown(this ILeafUI ui)
        {
            ui.Shutdown();
            throw new LeafExtensionTerminatedException();
        }

        /// <summary>
        /// 检查是否安装APP并询问用户,如果处于不恰当情况,将停止LeafExtension执行流程
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="device"></param>
        /// <param name="packageName"></param>

        public static void AppPropertyCheck(this ILeafUI ui, IDevice device, string packageName)
        {
            return;
//            var classTextReader = ClassTextReader.GetReader(typeof(LeafUIHelper));
//#pragma warning disable CS0618 // 类型或成员已过时
//            bool isInstall = new PackageManager(device).IsInstall(packageName) == true;
//#pragma warning restore CS0618 // 类型或成员已过时
//            if (!isInstall)
//            {
//                bool? choice = ui.DoChoice(classTextReader["msg"], classTextReader["ok"], classTextReader["continue"], classTextReader["cancel"]);
//                if (choice == null || choice == true) ui.EShutdown();
//            }
        }

        /// <summary>
        /// 检查设备安卓版本
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="device"></param>
        /// <param name="minAndroidVersion"></param>
        /// <param name="maxAndroidVersion"></param>
        /// <param name="targetAndroidVersion"></param>
        public static void ECheckAndroidVersion(this ILeafUI ui, IDevice device,
            string minAndroidVersion = null, string maxAndroidVersion = null, string targetAndroidVersion = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 进行警告
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="warn"></param>
        public static void EWarn(this ILeafUI ui, string warn)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 进行是否抉择
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="msg"></param>
        /// <param name="btnYes"></param>
        /// <param name="btnNo"></param>
        /// <returns></returns>
        public static void EAgree(this ILeafUI ui, string msg, string btnYes = null, string btnNo = null)
        {
            if (!ui.DoYN(msg, btnYes, btnNo)) ui.EShutdown();
        }

        /// <summary>
        /// 进行可取消的是否抉择
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="msg"></param>
        /// <param name="btnYes"></param>
        /// <param name="btnNo"></param>
        /// <param name="btnCancel"></param>
        /// <returns></returns>
        public static bool? EChoice(this ILeafUI ui, string msg, string btnYes = null, string btnNo = null, string btnCancel = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
