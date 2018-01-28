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
        public static byte[] GetIcon(DeviceSerial device, String packageName)
        {
            var response = ACPRequestSender.SendRequest(device, ACP.ACP.CMD_GETICON + " " + packageName);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }
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
                        Name = j[1].ToString(),
                        IsSystemApp = j[2].ToString() == "0" ? true : false
                    });
                }
                return result;
            }
            return null;
        }
        public struct AppUsedSpaceInfo {
            public long DataSize { get; set; }

            public long CodeSize { get; set; }
            public long CacheSize { get; set; }
            public long TotalSize { get {
                    return DataSize + CodeSize + CodeSize;
                } }
        }
        public static AppUsedSpaceInfo GetAppUsedSpace(DeviceSerial serial, String packageName) {
            var result = new AppUsedSpaceInfo() { DataSize = -1, CacheSize = -1, CodeSize = -1 };
            try {
                var response = ACPRequestSender.SendRequest(serial, ACP.ACP.CMD_GETPKGINFO + " " + packageName);
                if (response.IsSuccessful)
                {
                    var json = JObject.Parse(Encoding.UTF8.GetString(response.Data));
                    result.CodeSize = long.Parse(json["codeSize"].ToString());
                    result.CacheSize = long.Parse(json["cacheSize"].ToString());
                    result.DataSize = long.Parse(json["dataSize"].ToString());
                }
            } catch (Exception ex) {
                Logger.T("exception on getting app used space",ex);
            }
            return result;
        }
    }
}
