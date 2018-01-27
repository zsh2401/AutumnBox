/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:16:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ACP;
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public sealed class PackageInfo
    {
        #region Property
        public bool IsExist
        {
            get
            {
                var result = PackageManagerShared.Executer.QuicklyShell(Owner, $"pm path {Name}");
                return result.IsSuccessful;
            }
        }
        public string MainActivity
        {
            get
            {
                if (!IsExist) { throw new PackageNotFoundException(Name); }
                var exeResult = PackageManagerShared.Executer.QuicklyShell(Owner, $"dumpsys package {Name}");
                var match = Regex.Match(exeResult.Output.ToString(), mainActivityPattern);
                if (match.Success)
                {
                    return match.Result("${result}");
                }
                else
                {
                    return null;
                }
            }
        }
        public string ApplicationName { get; private set; }
        public string Name { get; private set; }
        public long CacheSize { get; private set; } = -1;
        public long CodeSize { get; private set; } = -1;
        public long DataSize { get; private set; } = -1;
        #endregion
        public DeviceSerial Owner { get; private set; }

        private static readonly string mainActivityPattern = $"android.intent.action.MAIN:{Environment.NewLine}.+.+\u0020(?<result>.+)";
        public PackageInfo(DeviceSerial owner, string name)
        {
            this.Name = name;
            this.Owner = owner;
        }
        public void LoadMoreInfo(Action callback = null)
        {
            try
            {
                var response = new ACPRequestSender(Owner).SendRequest(ACP.ACP.CMD_GETPKGINFO + " " + Name);
                if (response.IsSuccessful == false) {
                    callback?.Invoke();
                    return;
                }
                string text = Encoding.UTF8.GetString(response.Data);
                var jObj = JObject.Parse(text);
                ApplicationName = jObj["name"].ToString();
                CodeSize = long.Parse(jObj["codeSize"].ToString());
                CacheSize = long.Parse(jObj["cacheSize"].ToString());
                DataSize = long.Parse(jObj["dataSize"].ToString());
            }
            catch (Exception ex)
            {
                Logger.T("getting package info failed", ex);
            }
            callback?.Invoke();
        }
    }
}