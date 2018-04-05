/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 21:35:19
** filename: DeviceBuildPropGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备Build.prop信息获取器
    /// </summary>
    public class DeviceBuildPropGetter
    {
        private readonly CommandExecuter executer;
        /// <summary>
        /// 绑定的设备
        /// </summary>
        public DeviceSerialNumber Serial { get; private set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="serial"></param>
        public DeviceBuildPropGetter(DeviceSerialNumber serial)
        {
            executer = new CommandExecuter();
            Serial = serial;
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
                return new Version(Get(BuildPropKeys.AndroidVersion));
            }
            catch
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
        public string GetBoard() {
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
            var exeResult = executer.QuicklyShell(Serial, $"getprop {key}");
            return exeResult.IsSuccessful ? exeResult.ToString() : null;
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
                var exeResult = executer.QuicklyShell(Serial, $"getprop");
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
