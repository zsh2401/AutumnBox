/*

* ==============================================================================
*
* Filename: ECmdLauncher
* Description: 
*
* Version: 1.0
* Created: 2020/8/9 3:42:01
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.Extensions.Essentials.Extensions
{
    [ExtName("Launch ADB-CMD", "zh-cn:启动ADB命令行")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtIcon("Resources.Icons.cmd.png")]
    class ECmdLauncher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IBaseApi baseApi)
        {
            baseApi.UnstableInternalApiCall("open_command_line", "cmd.exe");
        }
    }
}
