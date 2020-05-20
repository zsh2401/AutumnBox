#nullable enable
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// ExtensionInfo的一些拓展方法
    /// </summary>
    public static class ExtensionInfoExtension
    {
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static string Name(this IExtensionInfo extensionInfo)
        {
            const string DEFAULT_VALUE = "Unknown Extension";
            try
            {
                return extensionInfo.Metadata[ExtensionMetadataKeys.NAME]?.Invoke() as string ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 判断该拓展是否需要root
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static bool NeedRoot(this IExtensionInfo extensionInfo)
        {
            const bool DEFAULT_VALUE = false;
            try
            {
                return extensionInfo.Metadata[ExtensionMetadataKeys.ROOT]?.Invoke() as bool? ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static byte[] Icon(this IExtensionInfo extensionInfo)
        {
            try
            {
                string iconSource = extensionInfo.Metadata[ExtensionMetadataKeys.ICON]?.Invoke() as string ?? BASE64ICON;
                return ReadAsBase64(iconSource) ??
                    ReadAsResource(extensionInfo as ClassExtensionInfo, iconSource) ??
                    Convert.FromBase64String(iconSource);
            }
            catch
            {
                return Convert.FromBase64String(BASE64ICON);
            }


            static byte[]? ReadAsBase64(string source)
            {
                if (source.StartsWith("__atmb_ext_base64_icon"))
                {
                    return Convert.FromBase64String(source);
                }
                return null;
            }

            static byte[]? ReadAsResource(ClassExtensionInfo? ceinf, string source)
            {
                try
                {
                    if (ceinf != null)
                    {
                        string path = ceinf.ClassExtensionType.Assembly.GetName().Name + "." + source;
                        Stream stream = ceinf.ClassExtensionType.Assembly.GetManifestResourceStream(path);
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        return buffer;
                    }
                }
                catch { }
                return null;
            }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static int Priority(this IExtensionInfo extensionInfo)
        {
            const int DEFAULT_VALUE = 0;
            try
            {
                return (extensionInfo.Metadata[ExtensionMetadataKeys.PRIORITY]?.Invoke() as int?)
                    ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 获取所有者
        /// </summary>
        /// <returns></returns>
        public static string Author(this IExtensionInfo extensionInfo)
        {
            const string DEFAULT_VALUE = "Unknown Developer";
            try
            {
                return (extensionInfo.Metadata[ExtensionMetadataKeys.AUTH]?.Invoke() as string)
                    ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 获取所需的设备状态
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static DeviceState RequiredDeviceState(this IExtensionInfo extensionInfo)
        {
            const DeviceState DEFAULT_VALUE = AutumnBoxExtension.AllState;
            try
            {
                return extensionInfo.Metadata[ExtensionMetadataKeys.REQ_DEV_STATE]?.Invoke() as DeviceState?
                    ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 可运行检查
        /// </summary>
        /// <param name="extInf"></param>
        /// <param name="currentDevice"></param>
        /// <returns></returns>
        public static bool IsRunnableCheck(this IExtensionInfo extInf, IDevice? currentDevice)
        {
            const bool DEFAULT_VALUE = false;
            try
            {
                var attr = (extInf.Metadata[ExtensionMetadataKeys.RUNNABLE_POLICY]() as ExtRunnablePolicyAttribute);
                return attr!.IsRunnable(new RunnableCheckArgs(extInf, currentDevice));
            }
            catch (Exception e)
            {
                SLogger.Warn(typeof(ExtensionInfoExtension).Name, "Could not finish runnable check", e);
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 获取是否隐藏
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static bool Hidden(this IExtensionInfo extensionInfo)
        {
            const bool DEFAULT_VALUE = true;
            try
            {
                return extensionInfo.Metadata[ExtensionMetadataKeys.IS_HIDDEN]?.Invoke() as bool?
                    ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 获取是否是开发者模式扩展模块
        /// </summary>
        /// <returns></returns>
        public static bool DeveloperMode(this IExtensionInfo extensionInfo)
        {
            const bool DEFAULT_VALUE = true;
            try
            {
                return extensionInfo.Metadata[ExtensionMetadataKeys.IS_DEVELOPING]?.Invoke() as bool? ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <param name="extensionInfo"></param>
        /// <returns></returns>
        public static string[] Regions(this IExtensionInfo extensionInfo)
        {
            string[] DEFAULT_VALUE = new string[0];
            try
            {
                return extensionInfo.Metadata[ExtensionMetadataKeys.REGIONS]?.Invoke() as string[] ?? DEFAULT_VALUE;
            }
            catch
            {
                return DEFAULT_VALUE;
            }
        }

        /// <summary>
        /// BASE64图标
        /// </summary>
        private const string BASE64ICON = "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";
    }
}
