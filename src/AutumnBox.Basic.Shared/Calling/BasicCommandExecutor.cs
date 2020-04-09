using AutumnBox.Basic.Data;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    sealed class BasicCommandExecutor : INotifyOutput, IReceiveOutputByTo<BasicCommandExecutor>, IDisposable
    {
        public string PathEnv { get; set; }
        private bool disposed = false;
        private readonly object _executeLock = new object();

        public event OutputReceivedEventHandler OutputReceived;

        private Process CurrentProcess { get; set; }
        public Task<ICommandResult> ExecuteAsync(string fileName, params string[] args)
        {
            ThrowIfDisposed();
            return Task.Run(() =>
            {
                return Execute(fileName, args);
            });
        }
        public ICommandResult Execute(string fileName, params string[] args)
        {
            ThrowIfDisposed();
            var pInfo = new ProcessStartInfo()
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = fileName,
                Arguments = string.Join(" ", args),
            };
            pInfo.EnvironmentVariables["path"] = $"{PathEnv};{pInfo.EnvironmentVariables["path"]}";
            throw new NotImplementedException();
        }
        public void KillCurrent()
        {
            ThrowIfDisposed();
        }
        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(BasicCommandExecutor));
            }
        }
        public void Dispose()
        {
            disposed = true;
            throw new NotImplementedException();
        }

        public BasicCommandExecutor To(Action<OutputReceivedEventArgs> callback)
        {
            throw new NotImplementedException();
        }
    }
}
