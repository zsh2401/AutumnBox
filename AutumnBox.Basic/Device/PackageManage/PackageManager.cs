/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 10:45:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ACP;
using AutumnBox.Basic.Executer;
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
            var builder = new AcpCommand.Builder
            {
                BaseCommand = Acp.CMD_GETICON
            };
            builder.SetArgs(packageName);
            var response = AcpCommunicator.GetAcpCommunicator(device).SendCommand(builder.ToCommand());
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
            try
            {
                var builder = new AcpCommand.Builder
                {
                    BaseCommand = Acp.CMD_PKGS
                };
                var response = AcpCommunicator.GetAcpCommunicator(serial).SendCommand(builder.ToCommand());
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
            catch (Exception ex)
            {
                Logger.D("GetPackages() fail",ex);
                return null;
            }
        }
        public struct AppUsedSpaceInfo {
            public long DataSize { get; set; }
            public long CodeSize { get; set; }
            public long CacheSize { get; set; }
            public long TotalSize { get {
                    return DataSize + CodeSize + CodeSize;
                } }
        }
        public static bool UninstallApp(DeviceSerial device, string packageName) {
            var exeResult = PackageManagerShared.Executer.Execute(Command.MakeForAdb(device, "uninstall " + packageName));
            return !exeResult.Output.Contains("Failure");
        }
        public static bool CleanAppData(DeviceSerial device, string packageName) {
            var exeResult = PackageManagerShared.Executer.QuicklyShell(device, "pm clear " + packageName);
            Logger.D($"clean {packageName} data success?{exeResult.IsSuccessful}");
            return exeResult.IsSuccessful;
        }
        public static AppUsedSpaceInfo GetAppUsedSpace(DeviceSerial serial, String packageName) {
            var result = new AppUsedSpaceInfo() { DataSize = -1, CacheSize = -1, CodeSize = -1 };
            try {
                var builder = new AcpCommand.Builder
                {
                    BaseCommand = Acp.CMD_GETPKGINFO
                };
                builder.SetArgs(packageName);
                var response = AcpCommunicator.GetAcpCommunicator(serial).SendCommand(builder.ToCommand());
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
