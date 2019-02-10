using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.OpenFramework.Wrapper
{
    /// <summary>
    /// ClassExtension scanner
    /// </summary>
    public sealed class ClassExtensionScanner : Context
    {
        /// <summary>
        /// 扫描设置
        /// </summary>
        public enum ScanOption
        {
            /// <summary>
            /// 扫描信息
            /// </summary>
            Informations = 1,
            /// <summary>
            /// 扫描切面
            /// </summary>
            BeforeCreatingAspect = 1 << 1
        }
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
        public void Scan(ScanOption options)
        {
            if (options.HasFlag(ScanOption.Informations))
            {
                ScanInformations();
            }
            if (options.HasFlag(ScanOption.BeforeCreatingAspect))
            {
                ScanAspects();
            }
        }
        private void ScanAspects()
        {
            Type interfaceType = typeof(IBeforeCreatingAspect);
            BeforeCreatingAspects = (from attr in type.GetCustomAttributes(true)
                                     where interfaceType.IsAssignableFrom(attr.GetType())
                                     select (IBeforeCreatingAspect)attr).ToArray();
        }
        private void ScanInformations()
        {
            Type interfaceType = typeof(IInformationAttribute);
            Logger.Debug($"scanning {type}");
            var attrs = type.GetCustomAttributes(true);
            Logger.Debug($"scanned: " + attrs.Count());
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
        /// <summary>
        /// 被扫描出来的切面
        /// </summary>
        public IBeforeCreatingAspect[] BeforeCreatingAspects { get; private set; }
    }
}
