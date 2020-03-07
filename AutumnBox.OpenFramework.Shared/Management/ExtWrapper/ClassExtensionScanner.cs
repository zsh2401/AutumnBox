using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    /// <summary>
    /// ClassExtension scanner
    /// </summary>
    public sealed class ClassExtensionScanner
    {
        private readonly Type type;
        /// <summary>
        /// 创造
        /// </summary>
        /// <param name="type"></param>
        public ClassExtensionScanner(Type type)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }
        /// <summary>
        /// 执行扫描操作
        /// </summary>
        /// <param name="options"></param>
        public void Scan()
        {
            ScanInformations();
        }
        private void ScanInformations()
        {
            Type interfaceType = typeof(IInformationAttribute);
            var attrs = type.GetCustomAttributes(true);
            Informations = new Dictionary<string, IInformationAttribute>();
            var informatons = from attr in attrs
                              where interfaceType.IsAssignableFrom(attr.GetType())
                              select (IInformationAttribute)attr;
            foreach (var info in informatons)
            {
                Informations[info.Key] = info;
            }
        }
        /// <summary>
        /// 被扫描出来的信息
        /// </summary>
        public Dictionary<string, IInformationAttribute> Informations { get; private set; }
    }
}
