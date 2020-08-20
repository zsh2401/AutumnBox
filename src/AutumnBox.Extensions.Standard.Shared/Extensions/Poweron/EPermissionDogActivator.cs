/*

* ==============================================================================
*
* Filename: EPermissionDogActivator
* Description: 
*
* Version: 1.0
* Created: 2020/8/9 3:32:52
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.Extensions.Standard.Extensions.Poweron
{
    [ExtName("Permission Dog Activator", "zh-cn:权限狗ADB模式")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.permissiondog")]
    [ExtAuth("zsh2401")]
    class EPermissionDogActivator : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI _ui, IDevice device, ICommandExecutor _executor)
        {
            using var ui = _ui;
            ui.Show();
            using var executor = _executor;
            executor.OutputReceived += (s, e) => ui.Println(e.Text);
            var result = executor.AdbShell(device, "sh /storage/emulated/0/Android/data/com.web1n.permissiondog/files/starter.sh");
            ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
        }
    }
}
