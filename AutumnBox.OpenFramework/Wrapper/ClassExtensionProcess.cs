/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/22 20:41:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Wrapper
{
    public sealed class ClassExtensionProcess : MarshalByRefObject, IExtensionProcess, IDisposable
    {
        private readonly Context ctx;
        private readonly Type extensionType;
        private readonly IDevice targetDevice;

        /// <summary>
        /// 创建实例前的切面
        /// </summary>
        public ExtBeforeCreateAspectAttribute[] BeforeCreatingAspects
        {
            set { bca = value; }
            get
            {
                if (bca == null)
                {
                    var attrs = Attribute.GetCustomAttributes(extensionType, typeof(ExtBeforeCreateAspectAttribute), true);
                    bca = (ExtBeforeCreateAspectAttribute[])attrs;
                }
                return bca;
            }
        }
        public ExtBeforeCreateAspectAttribute[] bca;

        private AutumnBoxExtension Instance { get; set; }
        private readonly IExtensionWrapper wrapper;

        public ClassExtensionProcess(IExtensionWrapper wrapper, Type extType, IDevice targetDevice = null)
        {
            this.ctx = (Context)wrapper;
            this.wrapper = wrapper;
            this.extensionType = extType;
            this.targetDevice = targetDevice;
        }

        private bool ExecuteBeforeCreatingInstanceAspect()
        {
            var args = new ExtBeforeCreateArgs()
            {
                Context = ctx,
                ExtType = extensionType,
                Prevent = false,
                TargetDevice = targetDevice
            };
            foreach (var aspect in BeforeCreatingAspects)
            {
                aspect.Before(args);
                if (args.Prevent)
                {
                    return false;
                }
            }
            return true;
        }

        private void CreateInstance()
        {
            Instance = (AutumnBoxExtension)Activator.CreateInstance(extensionType);
        }

        private void InjectProperty()
        {
            var args = new ExtensionArgs()
            {
                Wrapper = wrapper,
                CurrentProcess = this,
                TargetDevice = targetDevice
            };
            Instance.OnCreate(args);
        }

        private int ExecuteMainMethod()
        {
            Trace.WriteLine(GetHashCode().ToString());
            try
            {
                return Instance.Main();
            }
            catch (Exception ex)
            {
                ctx.Logger.Warn($"[Extension] {wrapper.Info.Name} was threw a exception", ex);
                ctx.App.RunOnUIThread(() =>
                {
                    string stoppedMsg = $"{wrapper.Info.Name} {ctx.App.GetPublicResouce<String>("OpenFxExtensionFailed")}";
                    ctx.Ux.Error(stoppedMsg);
                });
                return AutumnBoxExtension.ERR;
            }

        }

        private ProcessState State = ProcessState.Ready;
        private enum ProcessState
        {
            Ready,
            Running,
            Exited
        }
        public int ExitCode { get; private set; } = -1;
        public void Start()
        {
            Task.Run(() =>
            {
                MainFlowTryCatch();
            });
        }
        public int WaitForExit()
        {
            if (State == ProcessState.Ready)
            {
                throw new InvalidOperationException("Process is finished");
            }
            while (State == ProcessState.Running) ;
            return ExitCode;
        }
        private void MainFlowTryCatch()
        {
            try
            {
                ExitCode = MainFlow();
            }
            catch (Exception ex)
            {
                ExitCode = AutumnBoxExtension.ERR;
                ctx.Logger.Warn("Exception on MainFlow()", ex);
            }
            Dispose();
            State = ProcessState.Exited;
        }

        private bool executingMainMethod = false;
        private bool isStopped = false;

        private int MainFlow()
        {
            if (State != ProcessState.Ready)
            {
                throw new InvalidOperationException();
            }
            State = ProcessState.Running;
            if (!ExecuteBeforeCreatingInstanceAspect())
            {
                return -1;
            }
            CreateInstance();
            InjectProperty();
            int exitCode = AutumnBoxExtension.ERR_CANCELED_BY_USER;
            executingMainMethod = true;
            Task.Run(() =>
            {
                exitCode = ExecuteMainMethod();
                executingMainMethod = false;
            });
            while (executingMainMethod && !isStopped) ;
            ExitCode = exitCode;
            if (isStopped)
            {
                return exitCode;
            }
            var finishedArgs = new ExtensionFinishedArgs()
            {
                ExitCode = isStopped ? AutumnBoxExtension.ERR_CANCELED_BY_USER : exitCode
            };
            Instance.OnFinish(finishedArgs);
            Dispose();
            State = ProcessState.Exited;
            return exitCode;
        }

        public void Kill()
        {
            isStopped = false;
            try
            {
                isStopped = Instance.OnStopCommand();
            }
            catch (Exception ex)
            {
                throw new ExtensionCantBeStoppedException(wrapper.Info.Name + " cant be stopped", ex);
            }
            if (!isStopped)
            {
                throw new ExtensionCantBeStoppedException(wrapper.Info.Name + " cant be stopped");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //Instance?.OnDestory(new ExtensionDestoryArgs());
                }
                Instance?.OnDestory(new ExtensionDestoryArgs());
                BeforeCreatingAspects = null;
                Instance = null;
                disposedValue = true;
            }
        }

        ~ClassExtensionProcess()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
