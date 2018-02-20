/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 21:35:19
** filename: DeviceBuildPropGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    public class DeviceBuildPropGetter
    {
        private readonly CommandExecuter executer;
        public DeviceSerial Serial { get; private set; }
        public DeviceBuildPropGetter(DeviceSerial serial)
        {
            executer = new CommandExecuter();
            Serial = serial;
        }
        public string GetProductName()
        {
            return Get(BuildPropKeys.ProductName);
        }
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
        public string GetModel()
        {
            return Get(BuildPropKeys.Model);
        }
        public string GetBrand()
        {
            return Get(BuildPropKeys.Brand);
        }
        public string GetBoard() {
            return Get(BuildPropKeys.Board);
        }
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
        public string Get(string key)
        {
            var exeResult = executer.QuicklyShell(Serial, $"getprop {key}");
            return exeResult.IsSuccessful ? exeResult.ToString() : null;
        }

        private const string propPattern = @"\[(?<pname>.+)\].+\[(?<pvalue>.+)\]";
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
