#nullable enable
using System;
using System.Threading.Tasks;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.ManagedAdb.CommandDriven;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 优化的命令执行器
    /// </summary>
    public class HestExecutor : ICommandExecutor, INotifyDisposed
    {
        /// <summary>
        /// 执行器被析构时触发
        /// </summary>
        public event EventHandler? Disposed;

        /// <summary>
        /// 开始执行一条命令时触发
        /// </summary>
        public event CommandExecutingEventHandler? CommandExecuting;

        /// <summary>
        /// 一条命令执行完毕时触发
        /// </summary>
        public event CommandExecutedEventHandler? CommandExecuted;

        /// <summary>
        /// 接收到输出信息时触发
        /// </summary>
        public event OutputReceivedEventHandler? OutputReceived;

        /// <summary>
        /// 执行锁,确保随时只有一条命令在执行
        /// </summary>
        private readonly object _executingLock = new object();

        /// <summary>
        /// 当前正在执行命令
        /// </summary>
        private ICommandProcedure? commandProcedure = null;

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public CommandResult Execute(string fileName, string args)
        {
            lock (_executingLock)
            {
                if (isDisposed) throw new ObjectDisposedException(nameof(HestExecutor));

                //记录开始时间
                DateTime start = DateTime.Now;
                //开始进程
                commandProcedure = BasicBooter.CommandProcedureManager.OpenCommand(fileName, args);
                commandProcedure.OutputReceived += OnOutputReceived;

                //触发事件
                CommandExecuting?.Invoke(this, new CommandExecutingEventArgs(fileName, args));
                var cmdResult = commandProcedure.Execute();
                commandProcedure.Dispose();
                //记录结束时间
                DateTime end = DateTime.Now;

                //构造结果对象
                var result = new CommandResult(cmdResult.ExitCode, cmdResult.Output);

                //触发结束事件
                CommandExecuted?.Invoke(this, new CommandExecutedEventArgs(fileName, args, result, end - start));

                //返回结果
                return result;
            };
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<CommandResult> ExecuteAsync(string fileName, string args)
        {
            return Task.Run(() => Execute(fileName, args));
        }

        /// <summary>
        /// 处理输出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOutputReceived(object sender, OutputReceivedEventArgs e)
        {
            this.OutputReceived?.Invoke(this, e);
        }

        /// <summary>
        /// 是否已析构的依据
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// 析构本执行器
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
            CancelCurrent();
            try { Disposed?.Invoke(this, new EventArgs()); } catch { }
        }

        /// <summary>
        /// 取消当前执行的任务
        /// </summary>
        public void CancelCurrent()
        {
            commandProcedure?.Dispose();
        }

    }
}
