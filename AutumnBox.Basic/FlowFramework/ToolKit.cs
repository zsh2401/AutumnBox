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
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Executer;
using System;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// FunctionFlow工具箱,主要在FuntionFlow.MainMethod使用
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public class ToolKit<TArgs> where TArgs : FlowArgs
    {
        /// <summary>
        /// 调用工具箱内的Executer向绑定的设备执行Adb命令并且返回执行结果
        /// </summary>
        public readonly Func<string, AdvanceOutput> Ae;
        /// <summary>
        /// 调用工具箱内的Executer向绑定的设备执行Fastboot命令并且返回执行结果
        /// </summary>
        public readonly Func<string, AdvanceOutput> Fe;
        /// <summary>
        /// 执行器
        /// </summary>
        public readonly CommandExecuter Executer;
        /// <summary>
        /// 具体的参数,参数类型由所属的FunctionFlow决定
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
            //Ae = (command) =>
            //{ return Executer.Execute(Command.MakeForAdb(Args.DevBasicInfo.Serial, command)); };
            //Fe = (command) =>
            //{ return Executer.Execute(Command.MakeForFastboot(Args.DevBasicInfo.Serial, command)); };
        }
    }
}
