#define USE_LOCAL_API
using Ionic.Zip;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl
{
    class UpdaterImpl : IUpdater
    {
        private readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
        private const string tmpFilePath = "tmp.zip";
        private const string updateTmpDir = "..\\update";
#if USE_LOCAL_API
        private const string api = "http://localhost:24010/api/update_dv/test.html";
#else
        private const string api = "http://atmb.top/api/update_dv";
#endif
        public async void Start(IProgressWindow prgWin)
        {
            UpdateInfo uInfo = null;
            prgWin.SetTip("正在获取更新信息", 10);
            await Task.Run(() => uInfo = UpdateInfo.Parse(webClient.DownloadString(api)));
            prgWin.SetUpdateContent($"{uInfo.Title}{Environment.NewLine}{uInfo.UpdateContent}");
            if (Directory.Exists(updateTmpDir)) { Directory.Delete(updateTmpDir,true); }
            if (!Directory.Exists(updateTmpDir)) Directory.CreateDirectory(updateTmpDir);
            prgWin.SetTip("正在下载更新", 30);
            await Task.Run(() => webClient.DownloadFile(uInfo.DownloadUrl, Path.Combine(updateTmpDir, tmpFilePath)));
            prgWin.SetTip("正在应用更新", 80);
            await Task.Run(() =>
            {
                using (var zip = new ZipFile("..\\update\\tmp.zip"))
                {
                    zip.ExtractAll(Path.Combine(updateTmpDir));
                }
            });
            Process.Start(new ProcessStartInfo("cmd.exe")
            {
                Arguments = "/c " +  uInfo.Bat,
                WorkingDirectory = "..\\",
            }).Start();
            prgWin.Finish();
        }
    }
}
