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
    using System.IO;
    /// <summary>
    /// 文件发送器
    /// </summary>
    public sealed class FileSender : FunctionModule
    {
        public event SingleFileSendedEventHandler sendSingleFinish;
        public FileArgs _Args { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this._Args = e.ModuleArgs as FileArgs;
            foreach (string file in _Args.files)
            {
                if (!File.Exists(file))
                {
                    throw new FileNotFoundException();
                }
            }
            for (int i = 0; i < _Args.files.Length; i++)
            {
                _Args.files[i].Replace('\\', '/');
            }
        }
        protected override OutputData MainMethod()
        {
            foreach (string filepath in _Args.files)
            {
                FileInfo fi = new FileInfo(filepath);
                Ae($"push \"{filepath}\" /sdcard/{fi.Name}");
                sendSingleFinish?.Invoke(this, new SingleFileSendedEventArgs(filepath));
            }
            return new OutputData();
        }
    }
}
