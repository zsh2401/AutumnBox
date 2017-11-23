/* =============================================================================*\
*
* Filename: ToolKit
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:05:36 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Args;
using System;

namespace AutumnBox.Basic.FlowFramework.Container
{
    public class ToolKit
    {
        public readonly Func<string, OutputData> Ae;
        public readonly Func<string, OutputData> Fe;
        public readonly CExecuter Executer;
        public FlowArgs Args;
        public ToolKit(FlowArgs args, CExecuter executer = null)
        {
            Args = args;
            Executer = executer ?? new CExecuter();
            Ae = (command) =>
            { return Executer.Execute(Command.MakeForAdb(Args.DevBasicInfo.Id, command)); };
            Fe = (command) =>
            { return Executer.Execute(Command.MakeForFastboot(Args.DevBasicInfo.Id, command)); };
        }
    }
}
