using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 组件名
    /// </summary>
    public struct ComponentName
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
        /// 字符串化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{PackageName}/{ClassName}";
        }
    }
}
