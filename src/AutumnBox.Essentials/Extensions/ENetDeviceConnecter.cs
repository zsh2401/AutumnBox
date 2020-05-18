/*

* ==============================================================================
*
* Filename: ENetDeviceConnecter
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 21:33:42
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System.Net;
using System.Text.RegularExpressions;

namespace AutumnBox.Essentials.Extensions
{
    [ExtIcon("Resources.Icons.wireless.png")]
    [ExtName("Connect to net device", "zh-cn:连接网络设备")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    class ENetDeviceConnecter : LeafExtensionBase
    {
        public class NetDeviceConnecterPolicy : ExtNormalRunnablePolicyAttribute
        {
            public override bool IsRunnable(RunnableCheckArgs args)
            {
                bool n = base.IsRunnable(args);
                bool isNetDevice = args.TargetDevice is NetDevice;
                return n && isNetDevice;
            }
        }
        [LMain]
        public void EntryPoint(ILeafUI ui, ICommandExecutor executor)
        {
            var textCarrier = new TextCarrier();
            using (ui)
            {
                ui.Show();
                string input = null;
                IPEndPoint endPoint = null;
                do
                {
                    if (input != null)//输入错误
                    {
                        if (!ui.DoYN(textCarrier.RxGetClassText("input_error"), textCarrier.RxGetClassText("input_retry"), textCarrier.RxGetClassText("input_cancel")))
                        {
                            ui.EShutdown();
                        }
                    }
                    input = ui.InputString(textCarrier.RxGetClassText("input_hint"), input ?? "192.168.XX.X:5555");
                    if (input == null)
                    {
                        ui.EShutdown();
                    }
                } while (!TryParse(input, out endPoint));
                ui.Closing += (s, e) =>
                {
                    executor.CancelCurrent();
                    return true;
                };
                executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                if (executor.Adb($"connect {endPoint}").ExitCode == 0)
                {
                    ui.Finish(StatusMessages.Success);
                }
                else
                {
                    ui.Finish(StatusMessages.Failed);
                }
            }
        }
        private static readonly Regex inputParseRegexEngine = new Regex(@"^(?<ip>\d{0,3}\.\d{1,3}\.\d{0,3}\.\d{0,3}):{0,1}(?<port>\d{0,5})$");
        private bool TryParse(string input, out IPEndPoint ipEndPoint)
        {
            if (input == null)
            {
                ipEndPoint = default;
                return false;
            }
            var match = inputParseRegexEngine.Match(input);
            if (match.Success && IPAddress.TryParse(match.Result("${ip}"), out IPAddress address))
            {
                int port = int.TryParse(match.Result("${port}"), out int tmpPort) ? tmpPort : 5555;
                if (port < 0 || port > ushort.MaxValue)
                {
                    ipEndPoint = default;
                    return false;
                }
                ipEndPoint = new IPEndPoint(address, port);
                return true;
            }
            else
            {
                ipEndPoint = default;
                return false;
            }
        }
        [ClassText("input_hint", "Input IPAdress and port (e.g: 192.168.0.100:555)", "zh-CN:请输入设备的网络地址以及端口号(192.168.0.100:5555),端口号不输入则为默认5555")]
        [ClassText("input_error", "Input ipadress or port is invaild.", "zh-CN:输入有误,请输入正确的IP地址,端口号可不输入,默认5555")]
        [ClassText("input_cancel", "Cancel", "zh-CN:取消")]
        [ClassText("input_retry", "Retry", "zh-CN:重新输入")]
        private class TextCarrier { }
    }
}
