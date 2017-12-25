/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 21:35:19
** filename: DeviceBuildPropGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public class DeviceBuildPropGetter : IDisposable
    {
        private readonly AndroidShell shell;
        public DeviceBuildPropGetter(Serial serial)
        {
            shell = new AndroidShell(serial);
            shell.Connect();
        }
        public string GetProductName()
        {
            var executeResult = shell.SafetyInput("getprop ro.product.name");
            return executeResult.IsSuccess ? executeResult.OutputData.ToString() : null;
        }
        public Version GetAndroidVersion()
        {
            try
            {
                var executeResult = shell.SafetyInput("getprop ro.build.version.release");
                return new Version(executeResult.OutputData.ToString());
            }
            catch
            {
                return null;
            }
        }
        public string GetModel()
        {
            var executeResult = shell.SafetyInput("getprop ro.product.model");
            return executeResult.IsSuccess ? executeResult.OutputData.ToString() : null;
        }
        public string GetBrand()
        {
            var executeResult = shell.SafetyInput("getprop ro.product.brand");
            return executeResult.IsSuccess ? executeResult.OutputData.ToString() : null;
        }
        public int? GetSdkVersion()
        {
            try
            {
                var executeResult = shell.SafetyInput("getprop ro.build.version.sdk");
                return int.Parse(executeResult.OutputData.ToString());
            }
            catch
            {
                return null;
            }
        }

        private const string propPattern = @"\[(?<pname>.+)\].+\[(?<pvalue>.+)\]";
        public Dictionary<string, string> GetFull()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var o = shell.SafetyInput("getprop");
                var matches = Regex.Matches(o.OutputData.ToString(), propPattern);
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

        public void Dispose()
        {
            shell.Disconnect();
            shell.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
