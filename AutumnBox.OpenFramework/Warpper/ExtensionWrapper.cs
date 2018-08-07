/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:35:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 标准的拓展模块包装器
    /// </summary>
    internal class ExtensionWrapper : Context, IExtensionWarpper
    {
        /// <summary>
        /// 上次运行的返回值
        /// </summary>
        public int LastReturnCode { get; private set; } = -1;
        /// <summary>
        /// 托管的拓展模块信息
        /// </summary>
        private ExtensionInfoGetter info;
        /// <summary>
        /// 托管的拓展模块实例
        /// </summary>
        private AutumnBoxExtension instance;
        /// <summary>
        /// 托管的拓展模块Type
        /// </summary>
        private readonly Type extType;
        /// <summary>
        /// 拓展模块名
        /// </summary>
        public string Name => info.Name;
        /// <summary>
        /// 拓展模块说明
        /// </summary>
        public string Desc => info.FullDesc;
        /// <summary>
        /// 拓展模块所有者
        /// </summary>
        public string Auth => info.Auth;
        /// <summary>
        /// 是否需要以管理员模式运行
        /// </summary>
        public bool RunAsAdmin => info.RunAsAdmin;
        public byte[] Icon => info.Icon;
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => Name + "'s warpper";

        public bool Usable
        {
            get
            {
                return BuildInfo.API_LEVEL >= info.MinApi;
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="t"></param>
        internal ExtensionWrapper(Type t)
        {
            extType = t;
            info = new ExtensionInfoGetter(this, t);
            info.Load();
        }
        /// <summary>
        /// 运行前检查
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public virtual ForerunCheckResult ForerunCheck(DeviceBasicInfo device)
        {
            ForerunCheckResult result;
            if (info.RequiredStates.HasFlag(device.State))
            {
                result = ForerunCheckResult.Ok;
            }
            else
            {
                result = ForerunCheckResult.DeviceStateNotRight;
            }
            return result;
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="device"></param>
        public virtual void Run(DeviceBasicInfo device)
        {
            if (!OperatingSystem.IsRunAsAdmin && RunAsAdmin)
            {
                bool runnable = false;
                App.RunOnUIThread(() =>
                {
                    var result = App.ShowChoiceBox("Warning",
                        "该模块需要秋之盒以管理模式运行,但目前并不是,是否重启秋之盒为管理员模式?");
                    runnable = result == Open.ChoiceBoxResult.Right;
                    if (!runnable) return;
                    AutumnBoxGuiApiProvider.Get().RestartAsAdmin();
                });
                if (!runnable) return;
            }
            if (instance != null)
            {
                App.RunOnUIThread(() =>
                {
                    App.ShowMessageBox("警告", "该拓展模块已在运行,你不能开多个该模块!");
                });
            }
            CreateInstance();
            InjetctProperty(device);
            MainFlow();
        }
        /// <summary>
        /// 创建实例
        /// </summary>
        private void CreateInstance()
        {
            instance = (AutumnBoxExtension)Activator.CreateInstance(extType);
        }
        /// <summary>
        /// 注入属性
        /// </summary>
        /// <param name="device"></param>
        private void InjetctProperty(DeviceBasicInfo device)
        {
            instance.TargetDevice = device;
            instance.ExtName = Name;
        }
        /// <summary>
        /// 主流程
        /// </summary>
        private void MainFlow()
        {
            Manager.RunningManager.Add(this);
            try
            {
                LastReturnCode = instance.Main();
            }
            catch (Exception ex)
            {
                Logger.Warn($"[Extension] {Name} was threw a exception", ex);
                LastReturnCode = 1;
                App.RunOnUIThread(() =>
                {
                    string stoppedMsg = $"{Name} {App.GetPublicResouce<String>("msgExtensionWasFailed")}";
                    App.ShowMessageBox("Notice", stoppedMsg);
                });
            }
            finally
            {
                instance = null;
            }
            Manager.RunningManager.Remove(this);
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public virtual bool Stop()
        {
            bool stopped = false;
            try
            {
                stopped = instance.OnStopCommand();
            }
            catch (Exception ex)
            {
                Logger.Warn("停止时发生异常", ex);
            }
            if (stopped == true)
            {
                instance = null;
                Manager.RunningManager.Remove(this);
            }
            return stopped;
        }
        /// <summary>
        /// 当摧毁时被调用
        /// </summary>
        public virtual void Destory()
        {

        }
        public void RunAsync(DeviceBasicInfo device, Action<IExtensionWarpper> callback = null)
        {
            Task.Run(() =>
            {
                Run(device);
                callback?.Invoke(this);
            });
        }
    }
}