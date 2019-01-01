using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.Extension;
using System.Net;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtDeveloperMode(true)]
    [ExtName("网络设备连接器")]
    [ExtRequiredDeviceStates(NoMatter)]
    class ENetDeviceConnecter : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            IPEndPoint endPoint = Data["endpoint"] as IPEndPoint;
            using (var executor = new CommandExecutor())
            {
                executor.To(OutputPrinter);
                int exitCode = executor.Adb($"connect {endPoint.Address}:{endPoint.Port}").ExitCode;
                if (exitCode == 0)
                {
                    CloseUI();
                }
                return exitCode;
            }
        }
    }
}
