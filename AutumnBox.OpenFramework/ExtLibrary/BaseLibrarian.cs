/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:58:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Warpper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.ExtLibrary
{
    /// <summary>
    /// 入口类基类
    /// </summary>
    public abstract class BaseLibrarian : Context, ILibrarian
    {
        internal BaseLibrarian()
        {
        }
        /// <summary>
        /// 管理的程序集
        /// </summary>
        public Assembly ManagedAssembly { get; protected set; }
        /// <summary>
        /// 根据程序集进行初始化
        /// </summary>
        /// <param name="assembly"></param>
        protected void Init(Assembly assembly)
        {
            ManagedAssembly = assembly;
            Logger.Debug($"Managed assembly {GetType().Assembly.GetName().Name}");
            Reload();
        }
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => ManagedAssembly.GetName().Name + "Entrance";
        /// <summary>
        /// 名字
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 最低的秋之盒SDK
        /// </summary>
        public abstract int MinSdk { get; }
        /// <summary>
        /// 目标的秋之盒SDK
        /// </summary>
        public abstract int TargetSdk { get; }
        /// <summary>
        /// 用来存储所有已加载的包装类
        /// </summary>
        protected List<IExtensionWarpper> loadedWarpper = new List<IExtensionWarpper>();
        /// <summary>
        /// 运行检查
        /// </summary>
        /// <returns></returns>
        public virtual bool Check()
        {
            return BuildInfo.SDK_VERSION >= MinSdk;
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
            DoReload(loadedWarpper);
        }
        /// <summary>
        /// 重新加载的实现
        /// </summary>
        /// <param name="warppers"></param>
        protected virtual void DoReload(List<IExtensionWarpper> warppers)
        {
            warppers.Clear();
            var types = from type in ManagedAssembly.GetExportedTypes()
                        where IsExt(type)
                        select type;
            foreach (var type in types)
            {
                try
                {
                    var tmp = GetWarpperFor(type);
                    if (tmp.Usable)
                    {
                        warppers.Add(tmp);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn($"an exception threw on create wappers for {type.Name}", ex);
                }
            }
        }
        /// <summary>
        /// 判断某个Type是否是秋之盒拓展
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected virtual bool IsExt(Type t)
        {
            var result = t.IsSubclassOf(typeof(AutumnBoxExtension));
            return result;
        }
        /// <summary>
        /// 获取该入口类管理的所有封装器
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IExtensionWarpper> GetWarppers()
        {
            return loadedWarpper;
        }
        /// <summary>
        /// 为某个拓展获取包装类
        /// </summary>
        /// <param name="extType"></param>
        /// <returns></returns>
        protected virtual IExtensionWarpper GetWarpperFor(Type extType)
        {
            return new ExtensionWrapper(extType);
        }
        /// <summary>
        /// 析构所有包装类
        /// </summary>
        protected virtual void DestoryWarppers()
        {
            foreach (var w in loadedWarpper)
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
            DestoryWarppers();
        }
    }
}
