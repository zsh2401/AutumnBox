/*

* ==============================================================================
*
* Filename: EPSLauncher
* Description: 
*
* Version: 1.0
* Created: 2020/8/9 3:52:46
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
    [ExtName("Launch ADB-CMD(PS)", "zh-cn:ADB-PS命令行")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtIcon("Resources.Icons.ps.png")]
    class EPSLauncher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IBaseApi baseApi)
        {
            baseApi.UnstableInternalApiCall("open_command_line", "powershell.exe");
        }
    }
}
