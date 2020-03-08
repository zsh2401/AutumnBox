using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.Wrapper;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    internal sealed class ExtensionTask : IExtensionTask, IDisposable
    {
        private readonly Type extensionType;

        public Thread Thread { get; set; }

        public object Result { get; private set; } = null;


        public int Id { get; internal set; }

        public bool IsRunning => Thread?.IsAlive == true;

        public IExtensionWrapper Wrapper { get; }

        public Dictionary<string, object> Args { get; set; } =
            new Dictionary<string, object>();

        public event EventHandler<TaskFinishedEventArgs> Finished;
        public event EventHandler<TaskStartedEventArgs> Started;

        private IExtension instance;

        public void Kill()
        {
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
            Started?.Invoke(this, new TaskStartedEventArgs());
        }

        private void Flow()
        {
            try
            {
                isRunning = true;
                instance = (IExtension)Activator.CreateInstance(extensionType);
                Result = instance.Main(Args);
            }
            catch (ThreadAbortException)
            {
                Result = null;
            }
            catch (Exception e)
            {
                var appManager = LakeProvider.Lake.Get<IAppManager>();
                Result = e;
                SLogger<ExtensionTask>.Warn($"{extensionType.Name}-extension error", e.InnerException);
                string fmt = appManager.GetPublicResouce<string>("OpenFxExceptionMsgTitleFmt");
                fmt = string.Format(fmt, Wrapper.Info.Name);
                string sketch = appManager.GetPublicResouce<string>("OpenFxExceptionSketch");
                appManager.ShowException(fmt, sketch, e.GetType() == typeof(TargetInvocationException) ? e.InnerException : e);
            }
            finally
            {
                instance.Dispose();
                isRunning = false;
                Finished?.Invoke(this, new TaskFinishedEventArgs(this));
            }
        }


        public void Shutdown(int exitCode)
        {
            Kill();
        }

        public void WaitForExit()
        {
            while (isRunning) ;
        }

        public ExtensionTask(Type extensionType)
        {
            this.extensionType = extensionType;
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
        //~ExtensionTask()
        //{
        //    // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //    Dispose(false);
        //}

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
