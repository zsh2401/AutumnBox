using System;
using System.Collections.Generic;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 组件名
    /// </summary>
    public struct ComponentName : IEquatable<ComponentName>
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 根据完整的类名获取
        /// 如 
        /// pkgName: com.example.test
        /// className : com.example.test.hello.TestClass
        /// 结果: com.example.test/com.example.test.hello.TestClass
        /// </summary>
        /// <param name="pkgName"></param>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static ComponentName FromFullClassName(string pkgName, string fullClassName)
        {
            return new ComponentName()
            {
                PackageName = pkgName,
                ClassName = fullClassName
            };
        }
        /// <summary>
        /// 根据简写的类名获取
        /// 如 
        /// pkgName: com.example.test
        /// className : hello.TestClass
        /// 结果 com.example.test/.hello.TestClass
        /// </summary>
        /// <param name="pkgName"></param>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static ComponentName FromSimplifiedClassName(string pkgName, string fullClassName)
        {
            return new ComponentName()
            {
                PackageName = pkgName,
                ClassName = "." + fullClassName,
            };
        }
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is ComponentName && Equals((ComponentName)obj);
        }
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ComponentName other)
        {
            return PackageName == other.PackageName &&
                   ClassName == other.ClassName;
        }
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 1343116764;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PackageName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClassName);
            return hashCode;
        }

        /// <summary>
        /// 字符串化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{PackageName}/{ClassName}";
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public static bool operator ==(ComponentName name1, ComponentName name2)
        {
            return name1.Equals(name2);
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public static bool operator !=(ComponentName name1, ComponentName name2)
        {
            return !(name1 == name2);
        }
    }
}
