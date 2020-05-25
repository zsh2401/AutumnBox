/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 21:35:19
** filename: DeviceBuildPropGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 设备Build.prop信息获取器
    /// </summary>
    [Obsolete("尽可能使用CachedBuilderReader")]
    public sealed class BuildReder
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return Get(key);
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public BuildReder(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        /// <summary>
        /// 获取产品名 类似Mi 6
        /// </summary>
        /// <returns></returns>
        public string GetProductName()
        {
            return Get(BuildKeys.ProductName);
        }

        /// <summary>
        /// 获取安卓版本 类似 8.0.0
        /// </summary>
        /// <returns></returns>
        [Obsolete("不可靠函数,建议使用Get(BuildPropKeys.AndroidVersion);获取字符串格式的版本号")]
        public Version GetAndroidVersion()
        {
            var verStr = Get(BuildKeys.AndroidVersion);
            if (Version.TryParse(verStr, out Version result1))
            {
                return result1;
            }
            else if (Version.TryParse(verStr + ".0", out Version result2))
            {
                return result2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取Model 类似sagit
        /// </summary>
        /// <returns></returns>
        public string GetModel()
        {
            return Get(BuildKeys.Model);
        }

        /// <summary>
        /// 获取Brand 类似Xiaomi
        /// </summary>
        /// <returns></returns>
        public string GetBrand()
        {
            return Get(BuildKeys.Brand);
        }

        /// <summary>
        /// 获取主板信息,但因为厂商原因,可能会是别的信息
        /// </summary>
        /// <returns></returns>
        public string GetBoard()
        {
            return Get(BuildKeys.Board);
        }

        /// <summary>
        /// 获取设备SDK版本
        /// </summary>
        /// <returns></returns>
        public int? GetSdkVersion()
        {
            try
            {
                return int.Parse(Get(BuildKeys.SdkVersion));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 自行指定KEY的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            var exeResult = executor.AdbShell(device, $"getprop {key}");
            return exeResult.ExitCode == 0 ? exeResult.Output.ToString() : null;
        }

        private const string propPattern = @"\[(?<pname>.+)\].+\[(?<pvalue>.+)\]";
        /// <summary>
        /// 获取所有的key与value
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAll()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var exeResult = executor.AdbShell(device, $"getprop");
                var matches = Regex.Matches(exeResult.ToString(), propPattern, RegexOptions.Multiline);
                foreach (Match match in matches)
                {
                    dict.Add(match.Result("${pname}"), match.Result("${pvalue}"));
                }
                return dict;
            }
            catch
            {
                return null;
            }
        }
    }
}
