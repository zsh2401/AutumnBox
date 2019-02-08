using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutumnBox.OpenFramework.Fast
{
    /// <summary>
    /// 一些过滤器
    /// </summary>
    public static class WrapperFilter
    {
        /// <summary>
        /// 根据所需状态过滤
        /// </summary>
        /// <param name="wrappers"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IEnumerable<IExtensionWrapper> State(this IEnumerable<IExtensionWrapper> wrappers, DeviceState state)
        {
            return from wrapper in wrappers
                   where wrapper.Info.RequiredDeviceStates.HasFlag(state) || wrapper.Info.RequiredDeviceStates == state
                   select wrapper;
        }

        /// <summary>
        /// 根据当前地区进行过滤
        /// </summary>
        /// <param name="wrappers"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IEnumerable<IExtensionWrapper> Region(this IEnumerable<IExtensionWrapper> wrappers, string region)
        {
            return from wrapper in wrappers
                   where wrapper.RegionOK(region)
                   select wrapper;
        }
        private static bool RegionOK(this IExtensionWrapper wrapper, string region)
        {
            if (wrapper.Info.Regions == null)
            {
                return true;
            }
            return wrapper.Info.Regions.Where((str) =>
            {
                return str.ToLower() == region;
            }).Count() > 0;
        }

        /// <summary>
        /// 根据开发者状态进行过滤
        /// </summary>
        /// <param name="wrappers"></param>
        /// <param name="nowIsDevMode"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IEnumerable<IExtensionWrapper> Dev(this IEnumerable<IExtensionWrapper> wrappers, bool nowIsDevMode)
        {
            return from wrapper in wrappers
                   where wrapper.IsPassedDevCheck(nowIsDevMode)
                   select wrapper;
        }
        private static bool IsPassedDevCheck(this IExtensionWrapper wrapper, bool nowIsDevMode)
        {
            if (wrapper.Info.TryGetValueType(ExtensionInformationKeys.IS_DEVELOPING, out bool isDevExt))
            {
                if (isDevExt)
                {
                    return nowIsDevMode;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 过滤掉隐藏的
        /// </summary>
        /// <param name="wrappers"></param>
        /// <returns></returns>
        public static IEnumerable<IExtensionWrapper> Hide(this IEnumerable<IExtensionWrapper> wrappers)
        {
            return from wrapper in wrappers
                   where wrapper.IsHide()
                   select wrapper;
        }
        private static bool IsHide(this IExtensionWrapper wrapper)
        {
            if (wrapper.Info.TryGetValueType(ExtensionInformationKeys.IS_HIDE, out bool result))
            {

                return !result;
            }
            else
            {
                return true;
            }
        }
    }
}
