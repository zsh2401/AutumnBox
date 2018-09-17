/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:58:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.Impl;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.ExtLibrary
{
    /// <summary>
    /// 入口类基类
    /// </summary>
    public abstract class AssemblyBasedLibrarian : Context, ILibrarian
    {
        /// <summary>
        /// 是否是无占用型加载
        /// </summary>
        public bool IsUnoccupiedLoading
        {
            get
            {
                if (ManagedAssembly == null)
                {
                    throw new NullReferenceException("Managed assembly was not loaded");
                }
                return (Manager.InternalManager as InternalManagerImpl)
                    .IsOnceAssembly(ManagedAssembly);
            }
        }
        /// <summary>
        /// 管理的程序集
        /// </summary>
        public Assembly ManagedAssembly { get; private set; }
        /// <summary>
        /// 根据程序集进行初始化
        /// </summary>
        /// <param name="assembly"></param>
        protected void LoadFrom(Assembly assembly)
        {
            if (ManagedAssembly != null)
            {
                throw new Exception("Assembly was inited once!");
            }
            ManagedAssembly = assembly;
            Logger.Debug($"Managed assembly {GetType().Assembly.GetName().Name}");
        }
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => ManagedAssembly.GetName().Name + " Librarian";
        /// <summary>
        /// 名字
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 最低的秋之盒API
        /// </summary>
        public abstract int MinApiLevel { get; }
        /// <summary>
        /// 目标的秋之盒API
        /// </summary>
        public abstract int TargetApiLevel { get; }
        /// <summary>
        /// 用来存储所有已加载的包装类
        /// </summary>
        protected List<IExtensionWrapper> loadedWrapper = new List<IExtensionWrapper>();
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
            DoReload(loadedWrapper);
        }
        /// <summary>
        /// 重新加载的实现
        /// </summary>
        /// <param name="wrappers"></param>
        protected virtual void DoReload(List<IExtensionWrapper> wrappers)
        {
            var types = GetExtTypes();
            wrappers.AddRange(GetNewWrappersFrom(types));
        }
        /// <summary>
        /// 获取未被加载过的包装类
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        protected virtual IEnumerable<IExtensionWrapper> GetNewWrappersFrom(IEnumerable<Type> types)
        {
            List<IExtensionWrapper> newWrappers = new List<IExtensionWrapper>();
            foreach (var type in types)
            {
                try
                {
                    var tmp = GetWrapperFor(type);
                    if (tmp.Check())
                    {
                        tmp.Ready();
                        newWrappers.Add(tmp);
                    }
                    else
                    {
                        tmp.Destory();
                    }
                }
                catch (WrapperAlreadyCreatedOnceException)
                {
                    Logger.Debug($"{type.Name}'s Wrappers was created once,skip it");
                }
                catch (Exception ex)
                {
                    Logger.Warn($"an exception threw on create wappers for {type.Name}", ex);
                }
            }
            return newWrappers;
        }
        /// <summary>
        /// 获取符合条件的拓展模块类型
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetExtTypes()
        {
            return from type in ManagedAssembly.GetExportedTypes()
                   where IsExt(type)
                   select type;
        }
        /// <summary>
        /// 判断某个Type是否是秋之盒拓展
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected virtual bool IsExt(Type t)
        {
            var result = t.IsSubclassOf(typeof(AutumnBoxExtension));
            var isAbstract = t.IsAbstract;
            return result && !isAbstract;
        }
        /// <summary>
        /// 获取该入口类管理的所有封装器
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IExtensionWrapper> GetWrappers()
        {
            return loadedWrapper;
        }
        /// <summary>
        /// 为某个拓展获取包装类
        /// </summary>
        /// <param name="extType"></param>
        /// <returns></returns>
        protected virtual IExtensionWrapper GetWrapperFor(Type extType)
        {
            return new ClassExtensionWrapper(extType);
        }
        /// <summary>
        /// 析构所有包装类
        /// </summary>
        protected virtual void DestoryWrappers()
        {
            foreach (var w in loadedWrapper)
            {
                try
                {
                    w.Destory();
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 当拓展模块程序集被卸载时调用
        /// </summary>
        public virtual void Destory()
        {
            DestoryWrappers();
        }
    }
}
