/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:35:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 标准的拓展模块包装器
    /// </summary>
    internal class ClassExtensionWrapper : Context, IExtensionWarpper
    {
        private static List<Type> warppedType = new List<Type>();
        public static bool IsWarpped(Type t)
        {
            if (warppedType.IndexOf(t) != -1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 上次运行的返回值
        /// </summary>
        public int LastReturnCode { get; private set; } = -1;
        ///// <summary>
        ///// 托管的拓展模块信息
        ///// </summary>
        //private ClassExtensionInfoGetter info;
        /// <summary>
        /// 托管的拓展模块实例
        /// </summary>
        private AutumnBoxExtension instance;
        /// <summary>
        /// 托管的拓展模块Type
        /// </summary>
        private readonly Type extType;
        ///// <summary>
        ///// 拓展模块名
        ///// </summary>
        //public string Name => info.Name;
        ///// <summary>
        ///// 拓展模块说明
        ///// </summary>
        //public string Desc => info.FullDesc;
        ///// <summary>
        ///// 拓展模块所有者
        ///// </summary>
        //public string Auth => info.Auth;
        ///// <summary>
        ///// 是否需要以管理员模式运行
        ///// </summary>
        //public bool RunAsAdmin => info.RunAsAdmin;
        ///// <summary>
        ///// 图标
        ///// </summary>
        //public byte[] Icon => info.Icon;
        ///// <summary>
        ///// 日志标签
        ///// </summary>
        //public override string LoggingTag => Name + "'s warpper";
        /// <summary>
        /// 经过检查后,确实可用
        /// </summary>
        public bool Usable
        {
            get
            {
                return BuildInfo.API_LEVEL >= Info.MinApi;
            }
        }
        private ExtBeforeCreateAspectAttribute[] BeforeCreateAspects
        {
            get
            {
                if (bca == null)
                {
                    var attrs = Attribute.GetCustomAttributes(extType, typeof(ExtBeforeCreateAspectAttribute), true);
                    bca = (ExtBeforeCreateAspectAttribute[])attrs;
                }
                return bca;
            }
        }
        private ExtBeforeCreateAspectAttribute[] bca;
        private ExtMainAsceptAttribute[] MainAsceptAttributes
        {
            get
            {
                if (ma == null)
                {
                    var attrs = Attribute.GetCustomAttributes(extType, typeof(ExtMainAsceptAttribute), true);
                    ma = (ExtMainAsceptAttribute[])attrs;
                }
                return ma;
            }
        }
        private ExtMainAsceptAttribute[] ma;
        public IExtInfoGetter Info { get; private set; }

        /// <summary>
        /// 创建检查,如果有问题就抛出异常
        /// </summary>
        /// <param name="t"></param>
        protected virtual void CreatedCheck(Type t)
        {
            int index = warppedType.IndexOf(t);
            if (index != -1)
            {
                throw new WarpperAlreadyCreatedOnceException();
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="t"></param>
        internal ClassExtensionWrapper(Type t)
        {
            CreatedCheck(t);
            extType = t;
            Info = new ClassExtensionInfoGetter(this, t);
            Info.Reload();
            warppedType.Add(t);
        }
        /// <summary>
        /// 运行前检查
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public virtual ForerunCheckResult ForerunCheck(DeviceBasicInfo device)
        {
            ForerunCheckResult result;
            if (Info.RequiredDeviceStates.HasFlag(device.State))
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
            if (!RunningCheck()) return;
            if (BeforeCreateInstance(device) == false) return;
            //if (!OperatingSystem.IsRunAsAdmin && RunAsAdmin)
            //{
            //    bool runnable = false;
            //    App.RunOnUIThread(() =>
            //    {
            //        var result = App.ShowChoiceBox("Warning",
            //            "该模块需要秋之盒以管理模式运行,但目前并不是,是否重启秋之盒为管理员模式?");
            //        runnable = result == Open.ChoiceBoxResult.Right;
            //        if (!runnable) return;
            //        AutumnBoxGuiApiProvider.Get().RestartAsAdmin();
            //    });
            //    if (!runnable) return;
            //}
            CreateInstance();
            InjetctProperty(device);
            if (BeforeMain(device)) return;
            MainFlow();
            AfterMain();
        }
        private bool RunningCheck()
        {
            if (instance != null)
            {
                App.RunOnUIThread(() =>
                {
                    App.ShowMessageBox("警告", "该拓展模块已在运行,你不能开多个该模块!");
                });
                return false;
            }
            return true;
        }
        private bool BeforeCreateInstance(DeviceBasicInfo targetDevice)
        {
            ExtBeforeCreateArgs args = new ExtBeforeCreateArgs()
            {
                TargetDevice = targetDevice,
                ExtType = this.extType,
                Prevent = false,
                Context =this,
            };
            foreach (var aspect in BeforeCreateAspects)
            {
                aspect.Before(args);
                if (args.Prevent) return false;
            }
            return true;
        }
        private bool BeforeMain(DeviceBasicInfo targetDevice)
        {
            BeforeArgs args = new BeforeArgs()
            {
                TargetDevice = targetDevice,
                Prevent = false,
                Context = this,
            };
            foreach (var aspect in MainAsceptAttributes)
            {
                aspect.Before(args);
                if (args.Prevent) return false;
            }
            return true;
        }
        private void AfterMain()
        {
            AfterArgs args = new AfterArgs()
            {
                Context = this,
            };
            foreach (var aspect in MainAsceptAttributes)
            {
                aspect.After(args);
            }
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
            instance.ExtName = Info.Name;
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
                Logger.Warn($"[Extension] {Info.Name} was threw a exception", ex);
                LastReturnCode = 1;
                App.RunOnUIThread(() =>
                {
                    string stoppedMsg = $"{Info.Name} {App.GetPublicResouce<String>("msgExtensionWasFailed")}";
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
            warppedType.Remove(extType);
        }
        public void RunAsync(DeviceBasicInfo device, Action<IExtensionWarpper> callback = null)
        {
            Task.Run(() =>
            {
                Run(device);
                callback?.Invoke(this);
            });
        }


        public override int GetHashCode()
        {
            return extType.GetHashCode();
        }

        public bool Equals(IExtensionWarpper other)
        {
            return other != null && other.GetHashCode() == GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj as IExtensionWarpper);
        }
    }
}