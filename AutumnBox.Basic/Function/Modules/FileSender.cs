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
        public FileArgs Args { get; private set; }
        
        public FileSender(FileArgs fileArg) : base() {
            this.Args= fileArg;
            foreach (string file in fileArg.files) {
                if (!File.Exists(file)) {
                    throw new FileNotFoundException();
                }
            }
            for (int i = 0;i < Args.files.Length; i++) {
                Args.files[i].Replace('\\','/');
            }
        }
        protected override OutputData MainMethod()
        {
            string filename;
            string[] x;
            foreach (string filepath in Args.files)
            {
                x = filepath.Split('/');
                filename = x[x.Length - 1];
                Ae($"push \"{filepath}\" /sdcard/{filename}");
                sendSingleFinish?.Invoke(this,new SingleFileSendedEventArgs(filepath));
            }
            return new OutputData();
        }
    }
}
