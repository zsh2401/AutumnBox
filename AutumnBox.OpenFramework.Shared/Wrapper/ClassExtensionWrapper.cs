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
using AutumnBox.OpenFramework.Running;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Wrapper
{
    /// <summary>
    /// 标准的拓展模块包装器
    /// </summary>
    public class ClassExtensionWrapper : Context, IExtensionWrapper
    {
        /// <summary>
        /// TAG
        /// </summary>
        public override string LoggingTag
        {
            get
            {
                try
                {
                    var name = Info.Name;
                    if (name == null)
                    {
                        return "ClassExtensionWrapper";
                    }
                    return Info.Name + "'s wrapper";
                }
                catch
                {
                    return "ClassExtensionWrapper";
                }
            }
        }

        #region static Wrapper checker
        /// <summary>
        /// 已经进行过包装的拓展模块类
        /// </summary>
        private static List<Type> warppedType = new List<Type>();
        #endregion
        /// <summary>
        /// 托管的拓展模块Type
        /// </summary>
        private readonly Type extType;
        /// <summary>
        /// 创建实例前的切面
        /// </summary>
        private IBeforeCreatingAspect[] BeforeCreateAspects
        {
            get
            {
                if (bca == null)
                {
                    var scanner = new ClassExtensionScanner(extType);
                    scanner.Scan(ClassExtensionScanner.ScanOption.BeforeCreatingAspect);
                    bca = scanner.BeforeCreatingAspects;
                }
                return bca;
            }
        }
        private IBeforeCreatingAspect[] bca;

        /// <summary>
        /// 拓展模块的信息获取器
        /// </summary>
        public IExtInfoGetter Info { get; protected set; }

        /// <summary>
        /// 创建检查,如果有问题就抛出异常
        /// </summary>
        /// <param name="t"></param>
        protected virtual void CreatedCheck(Type t)
        {
            int index = warppedType.IndexOf(t);
            if (index != -1)
            {
                throw new WrapperAlreadyCreatedOnceException();
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="t"></param>
        internal protected ClassExtensionWrapper(Type t)
        {
            CreatedCheck(t);
            extType = t;
            Info = new ClassExtensionInfoGetter(this, t);
            Info.Reload();
            warppedType.Add(t);
        }
        /// <summary>
        /// 当摧毁时被调用
        /// </summary>
        public virtual void Destory()
        {
            Logger.CDebug("Good bye");
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
        public bool Equals(IExtensionWrapper other)
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
            return base.Equals(obj as IExtensionWrapper);
        }

        /// <summary>
        /// 创建后的检查,返回false则视为不可用,将不会有任何其他调用
        /// </summary>
        /// <returns></returns>
        public virtual bool Check()
        {
            return BuildInfo.API_LEVEL >= Info.MinApi;
        }

        /// <summary>
        /// 通过检查后调用
        /// </summary>
        public virtual void Ready()
        {
            Logger.Info("ready");
        }

        private IExtensionThreadManager GetThreadManager()
        {
            var manager = GetService<IExtensionThreadManager>(ServicesNames.THREAD_MANAGER);
            return manager;
        }
        /// <summary>
        /// 获取拓展进程
        /// </summary>
        /// <returns></returns>
        public virtual IExtensionThread GetThread()
        {
            var mgr = GetThreadManager();
            return mgr.Allocate(this,extType);
        }
        #endregion
    }
}