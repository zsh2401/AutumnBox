using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutumnBox.OpenFramework.Running
{
    internal class ExtensionThread : IExtensionThread
    {
        private readonly Type extensionType;

        public Thread Thread { get; set; }

        public int ExitCode
        {
            get
            {
                return shutDownExitCode ?? _exitCode;
            }
            private set { _exitCode = value; }
        }
        private int _exitCode = (int)ExtensionExitCodes.Killed;

        private int? shutDownExitCode = null;

        public int Id { get; internal set; }

        public bool IsRunning => Thread?.IsAlive == true;

        public IExtensionWrapper Wrapper { get; }

        public Dictionary<string, object> Data { get; set; } =
            new Dictionary<string, object>()
            {
                {AtmbVisualExtension.KEY_CLOSE_FINISHED,false}
            };

        public event EventHandler<ThreadFinishedEventArgs> Finished;
        public event EventHandler<ThreadStartedEventArgs> Started;

        private IExtension instance;

        public void SendSignal(string signal, object value = null)
        {
            if (string.IsNullOrWhiteSpace(signal))
            {
                throw new ArgumentException("message", nameof(signal));
            }
            instance.ReceiveSignal(signal, value);
        }

        public void Kill()
        {
            try
            {
                SendSignal(Signals.COMMAND_STOP);
            }
            catch
            {
                return;
            }
            try
            {
                Thread.Abort();
            }
            catch (ThreadAbortException) { }
        }

        private bool isRunning = false;
        public void Start()
        {
            Thread = new Thread(() =>
            {
                try
                {
                    isRunning = true;
                    instance = (IExtension)Activator.CreateInstance(extensionType);
                    instance.ReceiveSignal(Signals.ON_CREATED, new ExtensionArgs(this, Wrapper));
                    ExitCode = instance.Main(Data);
                }
                catch (ThreadAbortException)
                {
                    ExitCode = (int)ExtensionExitCodes.Killed;
                }
                catch (Exception)
                {
                    ExitCode = (int)ExtensionExitCodes.ErrorUnknown;
                }
                finally
                {
                    isRunning = false;
                    Finished?.Invoke(this, new ThreadFinishedEventArgs(this));
                }
            });
            Thread.Start();
            Started?.Invoke(this, new ThreadStartedEventArgs());
        }

        public void Shutdown(int exitCode)
        {
            shutDownExitCode = exitCode;
            Kill();
        }

        public void WaitForExit()
        {
            while (isRunning) ;
        }

        public ExtensionThread(Type extensionType, IExtensionWrapper wrapper)
        {
            this.extensionType = extensionType;
            Wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
        }
    }
}
