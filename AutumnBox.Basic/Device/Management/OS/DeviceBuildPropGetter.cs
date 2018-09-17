/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 21:35:19
** filename: DeviceBuildPropGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Util.Debugging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 设备Build.prop信息获取器
    /// </summary>
    public class DeviceBuildPropGetter : DependOnDeviceObject
    {
        private readonly ILogger logger;
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return loaded[key];
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public DeviceBuildPropGetter(IDevice device) : base(device)
        {
            logger = new Logger<DeviceBuildPropGetter>();
        }

        /// <summary>
        /// 获取产品名 类似Mi 6
        /// </summary>
        /// <returns></returns>
        public string GetProductName()
        {
            return Get(BuildPropKeys.ProductName);
        }

        /// <summary>
        /// 获取安卓版本 类似 8.0.0
        /// </summary>
        /// <returns></returns>
        public Version GetAndroidVersion()
        {
            try
            {
                var verStr = Get(BuildPropKeys.AndroidVersion);
                logger.Debug( verStr);
                return new Version(verStr);
            }
            catch (Exception ex)
            {
                logger.Warn("Get android version failed", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取Model 类似sagit
        /// </summary>
        /// <returns></returns>
        public string GetModel()
        {
            return Get(BuildPropKeys.Model);
        }

        /// <summary>
        /// 获取Brand 类似Xiaomi
        /// </summary>
        /// <returns></returns>
        public string GetBrand()
        {
            return Get(BuildPropKeys.Brand);
        }

        /// <summary>
        /// 获取主板信息,但因为厂商原因,可能会是别的信息
        /// </summary>
        /// <returns></returns>
        public string GetBoard()
        {
            return Get(BuildPropKeys.Board);
        }

        /// <summary>
        /// 获取设备SDK版本
        /// </summary>
        /// <returns></returns>
        public int? GetSdkVersion()
        {
            try
            {
                return int.Parse(Get(BuildPropKeys.SdkVersion));
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
            var exeResult = Device.Shell($"getprop {key}");
            return exeResult.Item2 == 0 ? exeResult.ToString() : null;
        }

        private const string propPattern = @"\[(?<pname>.+)\].+\[(?<pvalue>.+)\]";
        /// <summary>
        /// 获取所有的key与value
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetFull()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var exeResult = Device.Shell($"getprop");
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
       
        private Dictionary<string, string> loaded;
        /// <summary>
        /// 重载
        /// </summary>
        public void Reload()
        {
            loaded = GetFull();
        }
    }
}
