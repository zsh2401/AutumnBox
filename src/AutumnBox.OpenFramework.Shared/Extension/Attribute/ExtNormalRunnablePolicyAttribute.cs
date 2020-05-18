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
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management.ExtInfo;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 普通的运行策略
    /// </summary>
    public class ExtNormalRunnablePolicyAttribute : ExtRunnablePolicyAttribute
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool IsRunnable(RunnableCheckArgs args)
        {
            SLogger.CDebug(this,$"is runnable check");
            DeviceState reqDeviceState = args.ExtensionInfo.RequiredDeviceState();
            bool isNM = reqDeviceState == AutumnBoxExtension.NoMatter;
            bool containsCurrentState = false;
            if (args.TargetDevice != null)
            {
                containsCurrentState = reqDeviceState.HasFlag(args.TargetDevice.State);
            }
            return isNM || containsCurrentState;
        }
    }
}
