using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.LeafExtension.Kit;

namespace AutumnBox.OpenFramework.LeafExtension.Fast
{
    /// <summary>
    /// LeafUI相关拓展函数
    /// </summary>
    public static class LeafUIHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="LeafTerminatedException"></exception>
        /// <param name="ui"></param>
        /// <param name="exitCode"></param>
        public static void EFinish(this ILeafUI ui, int exitCode = 0)
        {
            ui.Finish(exitCode);
            LeafExtensionHelper.EndCurrentLeafThread(null, exitCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="LeafTerminatedException"></exception>
        /// <param name="ui"></param>
        /// <param name="tip"></param>
        public static void EFinish(this ILeafUI ui, string tip)
        {
            ui.Finish(tip);
            LeafExtensionHelper.EndCurrentLeafThread(null);
        }
        /// <summary>
        /// Shutdown
        /// </summary>
        /// <param name="ui"></param>
        public static void EShutdown(this ILeafUI ui)
        {
            ui.Shutdown();
            LeafExtensionHelper.EndCurrentLeafThread(null);
        }
        /// <summary>
        /// 检查是否安装APP并询问用户,如果处于不恰当情况,将停止LeafExtension执行流程
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="device"></param>
        /// <param name="packageName"></param>
        public static void ECheckApp(this ILeafUI ui, IDevice device, string packageName)
        {
            throw new System.NotImplementedException();
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
        public static bool EYN(this ILeafUI ui, string msg, string btnYes = null, string btnNo = null)
        {
            throw new System.NotImplementedException();
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
