/*

* ==============================================================================
*
* Filename: NormalRunnablePolicyAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 15:08:23
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Management.ExtInfo;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 普通的运行策略
    /// </summary>
    public class ExtNormalRunnablePolicyAttribute : ExtRunnableProlicyAttribute
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool IsRunnable(RunnableCheckArgs args)
        {
            DeviceState reqDeviceState = args.ExtensionInfo.RequiredDeviceState();
            bool isNM = reqDeviceState == AutumnBoxExtension.NoMatter;
            bool containsCurrentState = false;
            if (args.TargetDevice != null)
            {
                containsCurrentState = args.TargetDevice.State.HasFlag(reqDeviceState);
            }
            return isNM | containsCurrentState;
        }
    }
}
