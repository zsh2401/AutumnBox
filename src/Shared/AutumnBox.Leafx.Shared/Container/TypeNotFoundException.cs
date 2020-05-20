using System;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// 表示无法根据类型找到组件的错误
    /// </summary>
    public class TypeNotFoundException : ComponentNotFoundException
    {
        /// <summary>
        /// 组件的类型
        /// </summary>
        public Type ComponentType { get; }

        /// <summary>
        /// 创建TypeNotFoundException的新实例
        /// </summary>
        /// <param name="t"></param>
        public TypeNotFoundException(Type t) :
            base($"Type {t.FullName} not found in lake")
        {
            ComponentType = t;
        }
    }
}
