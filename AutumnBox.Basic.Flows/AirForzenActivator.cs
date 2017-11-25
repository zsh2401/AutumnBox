/* =============================================================================*\
*
* Filename: AirForzenActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:06:48 (UTC+8:00)
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
using AutumnBox.Basic.Flows.States;

namespace AutumnBox.Basic.Flows
{
    public sealed class AirForzenActivator : IceActivator
    {
        private static readonly string _command =
            "dpm set-device-owner me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver";
        protected override string _defaultShellCommand => _command;
    }
}
