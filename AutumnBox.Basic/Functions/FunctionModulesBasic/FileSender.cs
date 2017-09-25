namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Event;
    using System.IO;
    /// <summary>
    /// 文件发送器
    /// </summary>
    public sealed class FileSender : FunctionModule
    {
        public event SimpleFinishEventHandler sendSingleFinish;
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
                sendSingleFinish?.Invoke();
            }
            return new OutputData();
        }
    }
}
