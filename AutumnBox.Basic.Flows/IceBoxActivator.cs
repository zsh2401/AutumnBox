/* =============================================================================*\
*
* Filename: IceBoxActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:27:17 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.Flows.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Container;

namespace AutumnBox.Basic.Flows
{
    public sealed class IceBoxActivator : DeviceOwnerSetter
    {
        //private static readonly
        //    string _defaultCommand = "dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver";
        public static readonly string AppPackageName = "com.catchingnow.icebox";
        protected override string PackageName => AppPackageName;

        protected override string ClassName => ".receiver.DPMReceiver";
    }
}
