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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private bool IsForceStopped { get; set; }
        private AutumnBoxExtension Instance { get; set; }
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
                    var attrs = Attribute.GetCustomAttributes(extType.GetMethod("Main"), typeof(ExtMainAsceptAttribute), true);
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
        public virtual ForerunCheckResult ForerunCheck(IDevice device)
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
        public virtual void Run(IDevice device)
        {
            if (!PreCheck()) return;//多开检测
            /*初始化局部属性*/
            State = ExtensionWarpperState.Running;
            IsForceStopped = false;
            LastReturnCode = -1;
            Logger.Debug("inited");
            //创建前检测
            if (!BeforeCreateInstance(device)) {
                State = ExtensionWarpperState.Ready;
                return;
            }
            //创建实例
            CreateInstance();
            //依赖注入
            InjetctProperty(device);
            //Main方法前检测
            if (BeforeMain(device))
            {
                //执行主流程
                MainFlow();
                //运行结束切面
                AfterMain();
            }
            //摧毁实例
            DestoryInstance();
            State = ExtensionWarpperState.Ready;
        }

        /// <summary>
        /// 异步运行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="callback"></param>
        public void RunAsync(IDevice device, Action<IExtensionWarpper> callback = null)
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
                    Ux.ShowMessageDialog("警告", "该拓展模块已在运行,你不能开多个该模块!");
                });
                return false;
            }
        }

        /// <summary>
        /// 运行实例创建前的切面函数
        /// </summary>
        /// <param name="targetDevice"></param>
        /// <returns></returns>
        private bool BeforeCreateInstance(IDevice targetDevice)
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
        private void CreateInstance()
        {
            Instance = (AutumnBoxExtension)Activator.CreateInstance(extType);
        }

        /// <summary>
        /// 注入属性
        /// </summary>
        /// <param name="instatcePkg"></param>
        /// <param name="device"></param>
        private void InjetctProperty(IDevice device)
        {
            Instance.TargetDevice = device;
            Instance.ExtName = Info.Name;
        }

        /// <summary>
        /// 运行Main的运行前切面函数
        /// </summary>
        /// <param name="targetDevice"></param>
        /// <returns></returns>
        private bool BeforeMain(IDevice targetDevice)
        {
            Logger.Debug("BeforeMain() executing");
            BeforeArgs args = new BeforeArgs(Instance)
            {
                ExtWarpper = this,
                TargetDevice = targetDevice,
                Prevent = false,
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
        private void MainFlow()
        {
            Logger.Debug("MainFlow()");
            Manager.RunningManager.Add(this);
            try
            {
                LastReturnCode = Instance.Main();
            }
            catch (Exception ex)
            {
                Logger.Warn($"[Extension] {Info.Name} was threw a exception", ex);
                LastReturnCode = AutumnBoxExtension.ERR;
                App.RunOnUIThread(() =>
                {
                    string stoppedMsg = $"{Info.Name} {App.GetPublicResouce<String>("msgExtensionWasFailed")}";
                    Ux.ShowMessageDialog("Notice", stoppedMsg);
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
            try
            {
                IsForceStopped = Instance.OnStopCommand();
            }
            catch
            {
                IsForceStopped = false;
            }
            return IsForceStopped;
        }

        /// <summary>
        /// 运行Main的运行后切面函数
        /// </summary>
        private void AfterMain()
        {
            Logger.Debug("AfterMain() executing");
            AfterArgs args = new AfterArgs(Instance)
            {
                ExtWarpper = this,
                ReturnCode = LastReturnCode,
                IsForceStopped = IsForceStopped
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
        private void DestoryInstance()
        {
            Logger.Debug("destoring instantces");
            Instance = null;
            State = ExtensionWarpperState.Ready;
        }
        #endregion

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