/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:01:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System.Threading;
#nullable enable
namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活黑阈", "en-us:Activate brevent by one key")]
    [ExtDesc("一键激活黑阈,但值得注意的是,这样的激活方式,在重启后将失效", "Activate the brevent service by one key")]
    [ExtPriority(ExtPriority.LOW)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    [ClassText("firstMsg",
        "This device seems to be the first time to activate the black domain. Please slide to the right in the mobile phone black domain interface, click the boot and then close this popup window.",
        "zh-cn:此设备似乎是第一次激活黑域,请在手机黑域界面向右滑,点击启动后再关闭此弹窗")]
    internal class EBreventActivator : LeafExtensionBase
    {
        /// <summary>
        /// 黑域激活脚本的路径
        /// </summary>
        private const string SH_PATH = "/data/data/me.piebridge.brevent/brevent.sh";

        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="_ui"></param>
        /// <param name="device"></param>
        /// <param name="_executor"></param>
        [LMain]
        public void EntryPoint(ILeafUI _ui, IDevice device, ICommandExecutor _executor)
        {
            //确保能够在正确的时机释放
            using var ui = _ui;
            using var executor = _executor;

            //展现UI
            ui.Show();
            
            //注册输出事件
            executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);

            //判断脚本是否释放
            if (executor.AdbShell(device, $"cat {SH_PATH}").ExitCode != 0)
            {
                //提醒用户打开黑域以释放脚本
                ui.ShowMessage(this.RxGetClassText("firstMsg"));
                //再等一会儿
                Thread.Sleep(2000);
            }

            //执行脚本
            if (executor.AdbShell(device, $"sh {SH_PATH}").ExitCode != 0)
            {
                ui.Finish(StatusMessages.Failed);
            }
            else
            {
                ui.Finish(StatusMessages.Success);
            }

        }
    }
}
