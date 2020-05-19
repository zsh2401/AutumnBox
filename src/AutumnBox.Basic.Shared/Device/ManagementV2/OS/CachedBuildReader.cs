using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 使用缓存机制的BuildProp读取器
    /// </summary>
    public class CachedBuildReader
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        private readonly Dictionary<string, string> cache = new Dictionary<string, string>();
        /// <summary>
        /// 构造一个使用缓存机制的BuildProp读取器
        /// </summary>
        /// <param name="device">目标设备</param>
        /// <param name="executor">执行器</param>
        /// <param name="refreshOnCreating">指示是否在创建时进行刷新</param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public CachedBuildReader(IDevice device, ICommandExecutor executor, bool refreshOnCreating = true)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
            if (refreshOnCreating)
            {
                Refresh();
            }
        }

        private const string propPattern = @"\[(?<pname>.+)\].+\[(?<pvalue>.+)\]";
        private static readonly Regex propRegex = new Regex(propPattern, RegexOptions.Multiline | RegexOptions.Compiled);
        /// <summary>
        /// 刷新缓存
        /// </summary>
        public void Refresh()
        {
            cache.Clear();
            var exeResult = executor.AdbShell(device, $"getprop").ThrowIfError();
            var matches = propRegex.Matches(exeResult.Output);
            foreach (Match match in matches)
            {
                cache.Add(match.Result("${pname}"), match.Result("${pvalue}"));
            }
        }
        /// <summary>
        /// 传入键获取值,索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get => Get(key);
        }
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="KeyNotFoundException">键不存在</exception>
        /// <exception cref="ArgumentNullException">key值为NULL</exception>
        /// <returns></returns>
        public string Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            try
            {
                return cache[key];
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("There is no value of the key", e);
            }
        }
        /// <summary>
        /// 尝试根据键获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>是否成功</returns>
        public bool TryGet(string key, out string value)
        {
            try
            {
                value = Get(key);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
    }
}
