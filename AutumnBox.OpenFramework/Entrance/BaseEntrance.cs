/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:58:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Entrance
{
    /// <summary>
    /// 入口类基类
    /// </summary>
    public abstract class BaseEntrance : Context, IEntrance, IReloadable
    {
        /// <summary>
        /// 管理的程序集
        /// </summary>
        public Assembly ManagedAssembly { get; protected set; }
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => "";
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
        protected List<IExtensionWarpper> warppers = new List<IExtensionWarpper>();
        /// <summary>
        /// 仅用于继承的构造器
        /// </summary>
        internal BaseEntrance() { }
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
            warppers.Clear();
            var types = from type in ManagedAssembly.GetExportedTypes()
                        where typeof(IAutumnBoxExtension).IsAssignableFrom(type)
                        select type;
            foreach (var type in types)
            {
                warppers.Add(new ExtensionWrapper(type));
            }
        }
        /// <summary>
        /// 获取该入口类管理的所有封装器
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IExtensionWarpper> GetWarppers()
        {
            return warppers;
        }
        /// <summary>
        /// 析构所有包装类
        /// </summary>
        protected virtual void DestoryWarppers()
        {
            foreach (var w in warppers)
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
        public virtual void Destory() {
            DestoryWarppers();
        }
    }
}
