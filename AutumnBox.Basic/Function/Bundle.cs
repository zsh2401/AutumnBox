/* =============================================================================*\
*
* Filename: Bundle
* Description: 
*
* Version: 1.0
* Created: 2017/11/19 16:34:00 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function
{
    public class Bundle
    {
    }
    public class BundleForCreate : Bundle
    {
        public ModuleArgs Args { get; private set; }
        public BundleForCreate(ModuleArgs args)
        {
            Args = args;
        }
    }
    public class ToolsBundle
    {
        public CExecuter Executer { get; private set; }
        public Func<string, OutputData> Ae { get; private set; }
        public Func<string, OutputData> Fe { get; private set; }
        public ModuleArgs Args { get; private set; }
        public string DeviceID { get { return Args.DeviceBasicInfo.Id; } }
        public ToolsBundle(CExecuter executer, ModuleArgs args)
        {
            Executer = executer;
            Args = args;
            Ae = (command) =>
            { return Executer.Execute(Command.MakeForAdb(Args.DeviceBasicInfo, command)); };
            Fe = (command) =>
            { return Executer.Execute(Command.MakeForFastboot(Args.DeviceBasicInfo, command)); };
        }
    }
    public class BundleForAnalyzeOutput
    {
        public Object Other { get; set; }
        public ExecuteResult Result { get; set; }
        public OutputData OutputData { get { return Result.OutputData; } }
    }
}
