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
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 标准的拓展模块包装器
    /// </summary>
    public class ClassExtensionWrapper : Context, IExtensionWarpper
    {
        #region static warpper checker
        /// <summary>
        /// 已经进行过包装的拓展模块类
        /// </summary>
        private static List<Type> warppedType = new List<Type>();
        /// <summary>
        /// 检查是否进行过包装
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsWarpped(Type t)
        {
            if (warppedType.IndexOf(t) != -1)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 实例包裹
        /// </summary>
        protected class InstancePackage
        {
            /// <summary>
            /// 拓展模块
            /// </summary>
            public AutumnBoxExtension Extension { get; set; }
            /// <summary>
            /// UI控制器
            /// </summary>
            public IExtensionUIController UIController { get; set; }
        }

        /// <summary>
        /// 停止处理函数
        /// </summary>
        protected Func<bool> StopHandler { get; set; }
        /// <summary>
        /// 上次运行的返回值
        /// </summary>
        public int LastReturnCode { get; private set; } = -1;
        /// <summary>
        /// 托管的拓展模块Type
        /// </summary>
        private readonly Type extType;
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
        /// <summary>
        /// 创建实例前的切面
        /// </summary>
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
        /// <summary>
        /// 主方法前的切面
        /// </summary>
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
        /// <summary>
        /// 拓展模块的信息获取器
        /// </summary>
        public IExtInfoGetter Info { get; protected set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ExtensionWarpperState State { get; protected set; }

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
            State = ExtensionWarpperState.Ready;
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
            Logger.Info(result.ToString());
            return result;
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="device"></param>
        public virtual void Run(DeviceBasicInfo device)
        {
            if (!PreCheck()) return;//多开检测
            /*初始化局部属性*/
            State = ExtensionWarpperState.Running;
            bool forceStopped = false;
            InstancePackage instance = new InstancePackage();
            //初始化停止器
            StopHandler = () =>
            {
                ForceStop(instance, ref forceStopped);
                return forceStopped;
            };
            //创建前检测
            if (!BeforeCreateInstance(device)) return;
            //创建实例
            CreateInstance(instance);
            //依赖注入
            InjetctProperty(instance, device);

            //Main方法前检测
            if (BeforeMain(device))
            {
                //UI控制器开始
                App.RunOnUIThread(() =>
                {
                    instance.UIController?.OnStart();
                });
                //执行主流程
                MainFlow(instance);
                //运行结束切面
                AfterMain();
                //UI控制器结束
                App.RunOnUIThread(() =>
                {
                    instance.UIController?.OnFinish(LastReturnCode, forceStopped);
                });
            }
            //摧毁实例
            DestoryInstance(instance);
        }

        /// <summary>
        /// 异步运行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="callback"></param>
        public void RunAsync(DeviceBasicInfo device, Action<IExtensionWarpper> callback = null)
        {
            Task.Run(() =>
            {
                Run(device);
                callback?.Invoke(this);
            });
        }

        /// <summary>
        /// 当摧毁时被调用
        /// </summary>
        public virtual void Destory()
        {
            warppedType.Remove(extType);
        }

        #region 标准的执行流程
        /// <summary>
        /// 多开检查
        /// </summary>
        /// <returns></returns>
        private bool PreCheck()
        {
            if (State == ExtensionWarpperState.Ready)
            {
                return true;
            }
            else
            {
                App.RunOnUIThread(() =>
                {
                    App.ShowMessageBox("警告", "该拓展模块已在运行,你不能开多个该模块!");
                });
                return false;
            }
        }

        /// <summary>
        /// 运行实例创建前的切面函数
        /// </summary>
        /// <param name="targetDevice"></param>
        /// <returns></returns>
        private bool BeforeCreateInstance(DeviceBasicInfo targetDevice)
        {
            Logger.Debug("BeforeCreateInstance() executing");
            ExtBeforeCreateArgs args = new ExtBeforeCreateArgs()
            {
                TargetDevice = targetDevice,
                ExtType = extType,
                Prevent = false,
                Context = this,
            };
            foreach (var aspect in BeforeCreateAspects)
            {
                aspect.Before(args);
                if (args.Prevent) return false;
            }
            Logger.Debug("BeforeCreateInstance() executed");
            return true;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        private void CreateInstance(InstancePackage instatcePkg)
        {
            instatcePkg.Extension = (AutumnBoxExtension)Activator.CreateInstance(extType);
            if (Info.Visual)
            {
                App.RunOnUIThread(() =>
                {
                    instatcePkg.UIController = App.GetUIControllerOf(this);
                });
            }
        }

        /// <summary>
        /// 注入属性
        /// </summary>
        /// <param name="instatcePkg"></param>
        /// <param name="device"></param>
        private void InjetctProperty(InstancePackage instatcePkg, DeviceBasicInfo device)
        {
            instatcePkg.Extension.TargetDevice = device;
            instatcePkg.Extension.ExtName = Info.Name;
            instatcePkg.Extension.ExtensionUIController = instatcePkg.UIController;
        }

        /// <summary>
        /// 运行Main的运行前切面函数
        /// </summary>
        /// <param name="targetDevice"></param>
        /// <returns></returns>
        private bool BeforeMain(DeviceBasicInfo targetDevice)
        {
            Logger.Debug("BeforeMain() executing");
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
            Logger.Debug("BeforeMain() executed");
            return true;
        }

        /// <summary>
        /// 主流程
        /// </summary>
        private void MainFlow(InstancePackage instatcePkg)
        {
            Logger.Debug("MainFlow()");
            Manager.RunningManager.Add(this);
            try
            {
                LastReturnCode = instatcePkg.Extension.Main();
            }
            catch (Exception ex)
            {
                Logger.Warn($"[Extension] {Info.Name} was threw a exception", ex);
                LastReturnCode = AutumnBoxExtension.ERR;
                App.RunOnUIThread(() =>
                {
                    string stoppedMsg = $"{Info.Name} {App.GetPublicResouce<String>("msgExtensionWasFailed")}";
                    App.ShowMessageBox("Notice", stoppedMsg);
                });
            }
            Manager.RunningManager.Remove(this);
            Logger.Debug("MainFlow() executed");
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public virtual bool Stop()
        {
            return StopHandler();
        }

        /// <summary>
        /// 运行Main的运行后切面函数
        /// </summary>
        private void AfterMain()
        {
            Logger.Debug("AfterMain() executing");
            AfterArgs args = new AfterArgs()
            {
                Context = this,
            };
            foreach (var aspect in MainAsceptAttributes)
            {
                aspect.After(args);
            }
            Logger.Debug("AfterMain() executed");
        }

        /// <summary>
        /// 摧毁相关实例
        /// </summary>
        private void DestoryInstance(InstancePackage instancePkg)
        {
            Logger.Debug("destoring instantces");
            instancePkg.Extension = null;
            instancePkg.UIController = null;
            StopHandler = null;
            State = ExtensionWarpperState.Ready;
        }
        #endregion
        private void ForceStop(InstancePackage instatcePkg, ref bool stopped)
        {
            try
            {
                stopped = instatcePkg.Extension.OnStopCommand();
                instatcePkg.UIController.Tip = "被终止";
            }
            catch (Exception ex)
            {
                Logger.Warn("", ex);
                stopped = false;
            }
        }

        #region Equals
        /// <summary>
        /// 获取HashCode,实际上是拓展模块类的HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return extType.GetHashCode();
        }
        /// <summary>
        /// 对比
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IExtensionWarpper other)
        {
            return other != null && other.GetHashCode() == GetHashCode();
        }
        /// <summary>
        /// 对比
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj as IExtensionWarpper);
        }
        #endregion
    }
}