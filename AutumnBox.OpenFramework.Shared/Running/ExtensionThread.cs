using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace AutumnBox.OpenFramework.Running
{
    internal sealed class ExtensionThread : Context, IExtensionThread,IDisposable
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
            try
            {
                Logger.Debug($"sending signal {signal} to {Wrapper.Info.Name}");
                instance.ReceiveSignal(signal, value);
            }
            catch (Exception e)
            {
                Logger.Debug($"a exception was thrown when {Wrapper.Info.Name} handling signal: {signal}", e);
            }
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
            Thread = new Thread(Flow);
            Thread.Start();
            Thread.IsBackground = true;
            Started?.Invoke(this, new ThreadStartedEventArgs());
        }

        private void Flow()
        {
            try
            {
                isRunning = true;
                instance = (IExtension)Activator.CreateInstance(extensionType);
                SendSignal(Signals.ON_CREATED, new ExtensionArgs(this, Wrapper));
                ExitCode = instance.Main(Data);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException is ClassExtensionBase.AspectPreventedException)
                {
                    ExitCode = (int)ExtensionExitCodes.CanceledByUser;
                }
                else
                {
                    ExitCode = (int)ExtensionExitCodes.Exception;
                    SendSignal(Signals.ON_EXCEPTION, e);
                    string fmt = App.GetPublicResouce<string>("OpenFxExceptionMsgTitleFmt");
                    fmt = string.Format(fmt, Wrapper.Info.Name);
                    string sketch = App.GetPublicResouce<string>("OpenFxExceptionSketch");
                    Ux.RunOnUIThread(() =>
                    {
                        BaseApi.ShowException(fmt, sketch, e.ToString());
                    });
                }
            }
            catch (ThreadAbortException)
            {
                ExitCode = (int)ExtensionExitCodes.Killed;
            }
            catch (Exception e)
            {
                ExitCode = (int)ExtensionExitCodes.Exception;
                SendSignal(Signals.ON_EXCEPTION, e);
                string fmt = App.GetPublicResouce<string>("OpenFxExceptionMsgTitleFmt");
                fmt = string.Format(fmt, Wrapper.Info.Name);
                string sketch = App.GetPublicResouce<string>("OpenFxExceptionSketch");
                Ux.RunOnUIThread(() =>
                {
                    BaseApi.ShowException(fmt, sketch, e.ToString());
                });
            }
            finally
            {
                SendSignal(Signals.COMMAND_DESTORY);
                isRunning = false;
                Finished?.Invoke(this, new ThreadFinishedEventArgs(this));
                
            }
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

        public ExtensionThread(SExtensionThreadManager threadManager, Type extensionType, IExtensionWrapper wrapper)
        {
            if (threadManager == null)
            {
                throw new ArgumentNullException(nameof(threadManager));
            }

            this.extensionType = extensionType;
            Wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                Thread = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~ExtensionThread()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
