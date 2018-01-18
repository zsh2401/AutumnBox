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
using System;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// FunctionFlow工具箱
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public class ToolKit<TArgs> where TArgs : FlowArgs
    {
        /// <summary>
        /// 向绑定的设备执行Adb命令并且返回执行结果
        /// </summary>
        public readonly Func<string, CommandExecuterResult> Ae;
        /// <summary>
        /// 向绑定的设备执行Fastboot命令并且返回执行结果
        /// </summary>
        public readonly Func<string, CommandExecuterResult> Fe;
        /// <summary>
        /// 执行器
        /// </summary>
        public readonly CommandExecuter Executer;
        /// <summary>
        /// 参数
        /// </summary>
        public TArgs Args;
        /// <summary>
        /// 构造工具箱
        /// </summary>
        /// <param name="args"></param>
        /// <param name="executer"></param>
        public ToolKit(TArgs args, CommandExecuter executer = null)
        {
            Args = args;
            Executer = executer ?? new CommandExecuter();
            Ae = (command) =>
            { return Executer.Execute(Command.MakeForAdb(Args.DevBasicInfo.Serial, command)); };
            Fe = (command) =>
            { return Executer.Execute(Command.MakeForFastboot(Args.DevBasicInfo.Serial, command)); };
        }
    }
}
