using AutumnBox.Debug;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace AutumnBox.Util
{
    /// <summary>
    /// 更新检测器,使用Check方法检测完成后会执行UpdateCheckFinish事件
    /// </summary>
    internal class UpdateChecker
    {
        public delegate void UpdateCheckFinishHandler(bool haveUpdate, VersionInfo updateVersionInfo);
        public event UpdateCheckFinishHandler UpdateCheckFinish;
        private const string UPDATE_CHECK_URL = "https://raw.githubusercontent.com/zsh2401/AutumnBox/master/Api/update2401.json";
        private const string LOCAL_UPDATE_CHECK_FILE = "../Api/update2401.json";
        private const string TAG = "UpdateChecker";
        public void Check()
        {
            if (UpdateCheckFinish == null)
            {
                throw new NotSetEventHandlerException("没有设置事件!这还检测个皮皮虾的更新啊!!!");
            }
            Thread UpdateCheckThread = new Thread(_Check);
            UpdateCheckThread.Name = "Update Check Thread";
            UpdateCheckThread.Start();
        }
        private void _Check()
        {
            try
            {
                VersionInfo newVersionInfo = GetUpdateInfo();
                if (new Version(StaticData.nowVersion.version) < new Version(newVersionInfo.version)//服务器端的版本号大于当前程序
                &&//并且
                Config.skipVersion != newVersionInfo.version)//没有设置跳过这个版本
                {
                    UpdateCheckFinish(true, newVersionInfo);
                }
                else
                {
                    UpdateCheckFinish(false, newVersionInfo);
                }
            }
            catch (WebException e) {
                Log.d(TAG,"Update Check Fail" + e.Message);
            }
        }
        private static VersionInfo GetUpdateInfo()
        {
            JObject j;
#if TEST_LOCAL_API
            j = JObject.Parse(File.ReadAllText(LOCAL_UPDATE_CHECK_FILE));
#else
            try
            {
                j = JObject.Parse(Tools.GetHtmlCode(UPDATE_CHECK_URL));
            }
            catch (WebException e){
                throw e;
            }
            
#endif
#if! DEBUG
            return new VersionInfo
            {
                version = j["Release"]["Version"].ToString(),
                content = j["Release"]["UpdateContent"].ToString(),
                time = new DateTime(
                    year: int.Parse(j["Release"]["Date"][0].ToString()),
                    month: int.Parse(j["Release"]["Date"][1].ToString()),
                    day: int.Parse(j["Release"]["Date"][2].ToString())),
                baiduPanDownloadUrl = j["Release"]["BaiduPan"].ToString(),
                githubReleaseDownloadUrl = j["Release"]["GithubRelease"].ToString()
            };
#elif DEBUG
            return new VersionInfo
            {
                version = j["Debug"]["Version"].ToString(),
                content = j["Debug"]["UpdateContent"].ToString(),
                time = new DateTime(
                    year: int.Parse(j["Debug"]["Date"][0].ToString()),
                    month: int.Parse(j["Debug"]["Date"][1].ToString()),
                    day: int.Parse(j["Debug"]["Date"][2].ToString())),
                baiduPanDownloadUrl = j["Debug"]["BaiduPan"].ToString(),
                githubReleaseDownloadUrl = j["Debug"]["GithubRelease"].ToString()
            };
#endif
        }
    }
}

