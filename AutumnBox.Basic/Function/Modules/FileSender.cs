/* =============================================================================*\
*
* Filename: FileSender.cs
* Description: 
*
* Version: 1.0
* Created: 9/5/2017 18:24:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Support.CstmDebug;
    using System.IO;
    /// <summary>
    /// 文件发送器
    /// </summary>
    public sealed class FileSender : FunctionModule
    {
        public FileSenderArgs _Args { get; private set; }

        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            this._Args = (FileSenderArgs)args;
            _Args.FilePath = _Args.FilePath.Replace('\\', '/');
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData
            {
                OutSender = this.Executer
            };
            FileInfo fi = new FileInfo(_Args.FilePath);
            Ae($"push \"{fi.FullName}\" \"{_Args.SavePath  + _Args.SaveName}\"");
            Logger.D(o.ToString());
            return o;
        }
    }
}
