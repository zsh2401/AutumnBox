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
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    [ExtName("连接网络设备")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [NetDeviceConnecterPolicy]
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
        public void EntryPoint(ILeafUI ui)
        {
            using (ui)
            {
                ui.Show();

            }
        }
    }
}
