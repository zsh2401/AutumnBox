/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 10:45:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ACP;
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public static class PackageManager
    {
        public static List<PackageBasicInfo> GetPackages(DeviceSerial serial)
        {
            var response = ACPRequestSender.SendRequest(serial, ACP.ACP.CMD_PKGS);
            if (response.IsSuccessful)
            {
                var text = Encoding.UTF8.GetString(response.Data);
                Logger.D(text);
                var json = JObject.Parse(text);
                JArray array = (JArray)json["pkgs"];
                List<PackageBasicInfo> result = new List<PackageBasicInfo>();
                foreach (JArray j in array)
                {
                    result.Add(new PackageBasicInfo()
                    {
                        PackageName = j[0].ToString(),
                        Name = j[1].ToString()
                    });
                }
                return result;
            }
            return null;
        }
    }
}
