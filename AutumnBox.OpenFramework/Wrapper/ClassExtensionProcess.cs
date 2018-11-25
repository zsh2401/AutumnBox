/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/22 20:41:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Wrapper
{
    /// <summary>
    /// 拓展模块运行时进程,此进程非彼进程
    /// </summary>
    //public sealed class ClassExtensionProcess : Context, IExtensionProcess, IDisposable
    //{
    //    private readonly Context ctx;
    //    private readonly Type extensionType;

    //    /// <summary>
    //    /// 创建实例前的切面
    //    /// </summary>
    //    public IBeforeCreatingAspect[] BeforeCreatingAspects
    //    {
    //        set { bca = value; }
    //        get
    //        {
    //            if (bca == null)
    //            {
    //                var scanner = new ClassExtensionScanner(extensionType);
    //                scanner.Scan(ClassExtensionScanner.ScanOption.BeforeCreatingAspect);
    //                bca = scanner.BeforeCreatingAspects;
    //                Debug.WriteLine("aspects's count:" + bca.Count());

    //            }
    //            return bca;
    //        }
    //    }
    //    private IBeforeCreatingAspect[] bca;

    //    private IClassExtension Instance { get; set; }
    //    private readonly IExtensionWrapper wrapper;
    //    /// <summary>
    //    /// 构造类
    //    /// </summary>
    //    /// <param name="wrapper"></param>
    //    /// <param name="extType"></param>
    //    public ClassExtensionProcess(IExtensionWrapper wrapper, Type extType)
    //    {
    //        this.ctx = (Context)wrapper;
    //        this.wrapper = wrapper;
    //        this.extensionType = extType;
    //    }

    //    private bool ExecuteBeforeCreatingInstanceAspect()
    //    {
    //        var args = new BeforeCreatingAspectArgs()
    //        {
    //            Context = ctx,
    //            ExtensionType = extensionType,
    //            TargetDevice = GetService<IDeviceSelector>(ServicesNames.DEVICE_SELECTOR).GetCurrent(this)
    //        };
    //        bool canContinue = true;
    //        foreach (var aspect in BeforeCreatingAspects)
    //        {
    //            aspect.BeforeCreating(args, ref canContinue);
    //            if (!canContinue)
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }

    //    private void CreateInstance()
    //    {
    //        Instance = (IClassExtension)Activator.CreateInstance(extensionType);
    //    }

    //    private void InjectProperty()
    //    {
    //        var args = new ExtensionArgs()
    //        {
    //            Wrapper = wrapper,
    //            CurrentProcess = this,
    //            ExtractData = this.ExtractData
    //        };
    //        Instance.Init(ctx, args);
    //    }

    //    private int ExecuteMainMethod()
    //    {
    //        Trace.WriteLine(GetHashCode().ToString());
    //        try
    //        {
    //            return Instance.Run(ctx);
    //        }
    //        catch (ExtensionCanceledException)
    //        {
    //            return AutumnBoxExtension.ERR_CANCELED_BY_USER;
    //        }
    //        catch (Exception ex)
    //        {
    //            ctx.Logger.Warn($"[Extension] {wrapper.Info.Name} was threw a exception", ex);
    //            ctx.App.RunOnUIThread(() =>
    //            {
    //                string stoppedMsg = $"{wrapper.Info.Name} {ctx.App.GetPublicResouce<String>("OpenFxExtensionFailed")}";
    //                ctx.Ux.Error(stoppedMsg);
    //            });
    //            return AutumnBoxExtension.ERR;
    //        }

    //    }

    //    private ProcessState State = ProcessState.Ready;
    //    private enum ProcessState
    //    {
    //        Ready,
    //        Running,
    //        Exited
    //    }
    //    /// <summary>
    //    /// 返回码
    //    /// </summary>
    //    public int ExitCode { get; private set; } = -1;
    //    /// <summary>
    //    /// 拓展数据
    //    /// </summary>
    //    public Dictionary<string,object> ExtractData { get; set; }

    //    /// <summary>
    //    /// 开始执行
    //    /// </summary>
    //    public void Start()
    //    {
    //        Task.Run(() =>
    //        {
    //            MainFlowTryCatch();
    //        });
    //    }
    //    /// <summary>
    //    /// 等待结束
    //    /// </summary>
    //    /// <returns></returns>
    //    public int WaitForExit()
    //    {
    //        //if (State == ProcessState.Ready)
    //        //{
    //        //    throw new InvalidOperationException("Process is finished");
    //        //}
    //        while (State == ProcessState.Running) ;
    //        return ExitCode;
    //    }
    //    private void MainFlowTryCatch()
    //    {
    //        try
    //        {
    //            ExitCode = MainFlow();
    //        }
    //        catch (Exception ex)
    //        {
    //            ExitCode = AutumnBoxExtension.ERR;
    //            ctx.Logger.Warn("Exception on MainFlow()", ex);
    //        }
    //        Dispose();
    //        State = ProcessState.Exited;
    //    }

    //    private bool executingMainMethod = false;
    //    private bool isForceStopped = false;

    //    private int MainFlow()
    //    {
    //        if (State != ProcessState.Ready)
    //        {
    //            throw new InvalidOperationException();
    //        }
    //        State = ProcessState.Running;
    //        if (!ExecuteBeforeCreatingInstanceAspect())
    //        {
    //            return -1;
    //        }
    //        CreateInstance();
    //        InjectProperty();
    //        int exitCode = AutumnBoxExtension.ERR_CANCELED_BY_USER;
    //        executingMainMethod = true;
    //        Task.Run(() =>
    //        {
    //            exitCode = ExecuteMainMethod();
    //            executingMainMethod = false;
    //        });
    //        while (executingMainMethod && !isForceStopped)
    //        {
    //            Thread.Sleep(1000);
    //        }
    //        ExitCode = exitCode;
    //        var finishedArgs = new ExtensionFinishedArgs()
    //        {
    //            ExitCode = isForceStopped ? AutumnBoxExtension.ERR_CANCELED_BY_USER : exitCode,
    //            IsForceStopped = isForceStopped
    //        };
    //        Instance.Finish(ctx, finishedArgs);
    //        State = ProcessState.Exited;
    //        Dispose();
    //        return exitCode;
    //    }
    //    /// <summary>
    //    /// 试图杀死这个进程
    //    /// </summary>
    //    public void Kill()
    //    {
    //        isForceStopped = false;
    //        try
    //        {
    //            isForceStopped = Instance.TryStop(ctx, new ExtensionStopArgs());
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ExtensionCantBeStoppedException(wrapper.Info.Name + " cant be stopped", ex);
    //        }
    //        if (!isForceStopped)
    //        {
    //            throw new ExtensionCantBeStoppedException(wrapper.Info.Name + " cant be stopped");
    //        }
    //    }

    //    #region IDisposable Support
    //    private bool disposedValue = false;

    //    private void Dispose(bool disposing)
    //    {
    //        if (!disposedValue)
    //        {
    //            if (disposing)
    //            {
    //                //Instance?.OnDestory(new ExtensionDestoryArgs());
    //            }
    //            Instance?.Destory(ctx, new ExtensionDestoryArgs());
    //            BeforeCreatingAspects = null;
    //            Instance = null;
    //            disposedValue = true;
    //        }
    //    }
    //    /// <summary>
    //    /// 析构器
    //    /// </summary>
    //    ~ClassExtensionProcess()
    //    {
    //        Dispose(false);
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        //GC.SuppressFinalize(this);
    //    }
    //    #endregion
    //}
}
