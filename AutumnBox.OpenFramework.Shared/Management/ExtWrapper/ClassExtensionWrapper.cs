/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:35:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Management.ExtTask;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    /// <summary>
    /// 标准的拓展模块包装器
    /// </summary>
    public class ClassExtensionWrapper : IExtensionWrapper
    {

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
        /// 拓展模块的信息获取器
        /// </summary>
        public IExtensionInfoDictionary Info { get; protected set; }

        /// <summary>
        /// 所包装的拓展模块类型
        /// </summary>
        public Type ExtensionType => extType;

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
            Info = new ClassExtensionInfoReader(t);
            Info.Reload();
            warppedType.Add(t);
        }
        /// <summary>
        /// 当摧毁时被调用
        /// </summary>
        public virtual void Destory()
        {
            SLogger.Debug(Info.Name, "Good bye");
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
            SLogger.Info(Info.Name, "Ready");
        }
        #endregion
    }
}