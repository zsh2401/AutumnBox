/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:58:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AutumnBox.OpenFramework.Management.ExtLibrary.Impl
{
    /// <summary>
    /// 入口类基类
    /// </summary>
    public class AssemblyLibrarian : ILibrarian
    {
        /// <summary>
        /// 管理的程序集
        /// </summary>
        public Assembly ManagedAssembly { get; set; }

        /// <summary>
        /// 构造基于程序集的库管理器,具体的程序集将在稍后手动设置
        /// </summary>
        public AssemblyLibrarian()
        {
            this.ManagedAssembly = this.GetType().Assembly;
        }

        /// <summary>
        /// 名字
        /// </summary>
        public virtual string Name { get; } = "Unknown Librarian";

        /// <summary>
        /// 最低的秋之盒API
        /// </summary>
        public virtual int MinApiLevel { get; } = BuildInfo.API_LEVEL;

        /// <summary>
        /// 目标的秋之盒API
        /// </summary>
        public virtual int TargetApiLevel { get; } = BuildInfo.API_LEVEL;

        /// <summary>
        /// 获取版本号
        /// </summary>
        public virtual Version Version
        {
            get
            {
                return GetType().Assembly.GetName().Version;
            }
        }

        /// <summary>
        /// 拓展模块
        /// </summary>
        public IEnumerable<IExtensionInfo> Extensions { get; private set; }

        /// <summary>
        /// 运行检查
        /// </summary>
        /// <returns></returns>
        public virtual bool Check()
        {
            return BuildInfo.API_LEVEL >= MinApiLevel;
        }

        /// <summary>
        /// 当库管理器准备时调用
        /// </summary>
        public virtual void Ready()
        {
            SLogger.Info(GetType().Name, $"librarian {Name} 's ready");
        }


        /// <summary>
        /// 重载内部信息
        /// </summary>
        public virtual void Reload()
        {
            if (ManagedAssembly == null)
            {
                throw new NullReferenceException("Managed assembly not defined.");
            }
            RefreshExtensions();
        }

        public virtual void RefreshExtensions()
        {
            //Emtpy exist extensions record
            if (Extensions != null)
            {
                foreach (var ext in Extensions)
                {
                    var rext = new RegisteredExtensionInfo(ext, this);
                    LakeProvider.Lake.Get<ILibsManager>().Registry.Remove(rext);
                }
            }

            //Rescan and load
            Extensions = from type in ManagedAssembly.GetTypes()
                         where IsExt(type)
                         select CreateExtensionInfo(type);
            foreach (var ext in Extensions)
            {
                var rext = new RegisteredExtensionInfo(ext, this);
                LakeProvider.Lake.Get<ILibsManager>().Registry.Add(rext);
            }
        }
        /// <summary>
        /// 判断某个Type是否是秋之盒拓展
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected virtual bool IsExt(Type t)
        {
            Type extInterfaceType = typeof(IClassExtension);
            bool isImpl = extInterfaceType.IsAssignableFrom(t);
            var isAbstract = t.IsAbstract;
            return isImpl && !isAbstract;
        }

        /// <summary>
        /// 为某个拓展获取包装类
        /// </summary>
        /// <param name="extType"></param>
        /// <returns></returns>
        protected virtual IExtensionInfo CreateExtensionInfo(Type extType)
        {
            return ClassExtensionInfoCache.Acquire(extType);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Destory()
        {
            SLogger.Info(this, $"librarian {Name}'s destorying");
        }

        [AutoInject] IAppManager appManager;

        [AutoInject] INotificationManager notificationManager;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void DisplayMessage()
        {
            appManager.RunOnUIThread(() =>
            {
                notificationManager.Info($"{this.Name}-v{this.Version}\nLoaded {this.Extensions.Count()} extension(s)");
            });
        }
    }
}
