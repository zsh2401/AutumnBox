/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:58:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.ExtInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                throw new NullReferenceException("ManagedAssembly must be setted");
            }
            Extensions = from type in ManagedAssembly.GetTypes()
                         where IsExt(type)
                         select CreateExtensionInfo(type);
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
            return ClassExtensionInfo.GetByType(extType);
        }

        /// <summary>
        /// 当拓展模块程序集被卸载时调用
        /// </summary>
        public virtual void Destory()
        {
            SLogger.Info(this, $"librarian {Name}'s destorying");
        }
    }
}
