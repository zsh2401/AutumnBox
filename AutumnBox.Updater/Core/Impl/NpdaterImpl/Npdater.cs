/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 17:53:10 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutumnBox.Updater.Core.Impl.NpdaterImpl
{
    sealed class Npdater : IUpdater
    {
        private readonly IDiffer Differ = new NpdaterDiffer();
        private readonly IUpdateInfoGetter InfoGetter = new NpdateInfoGetter();
        private readonly IDownloader Downloader = new NpdaterDownloader();
        public void Start(IProgressWindow prgWin)
        {
            try
            {
                Flow(prgWin);
            }
            catch (Exception ex)
            {
                prgWin.AppendLog("下载失败" + ex.ToString());
                Debug.WriteLine(ex);
            }
        }
        private void Flow(IProgressWindow prgWin)
        {
            prgWin.AppendLog("获取信息中", 10);
            IUpdateInfo info = InfoGetter.Get();
            prgWin.AppendLog("获取信息完毕,正在解析", 20);
            prgWin.SetUpdateContent(info.UpdateContent);

            IEnumerable<IFile> needUpdateFile = Differ.Diff(info.Files, GetLocalFiles());
            if (needUpdateFile.Count() == 0)
            {
                prgWin.AppendLog("无需更新!", 100);
                Thread.Sleep(4000);
                prgWin.Finish();
                return;
            }
            int downloadingFile = 0;
            Downloader.DownloadedAFile += (s, e) =>
            {
                downloadingFile++;
                prgWin.SetProgress(20 + (100 / needUpdateFile.Count() * downloadingFile * 80));
                prgWin.AppendLog($"正在下载并更新{downloadingFile}/{needUpdateFile.Count()}");
            };

            Downloader.Download(needUpdateFile);
            prgWin.AppendLog("结束,三秒后退出", 100);
            Thread.Sleep(3000);
            prgWin.Finish();
        }
        private static IEnumerable<FileInfo> GetLocalFiles()
        {
            List<FileInfo> data = new List<FileInfo>();
            GetFiles(data, new DirectoryInfo(Environment.CurrentDirectory));
            return data;
        }
        private static void GetFiles(List<FileInfo> data, DirectoryInfo startPath)
        {
            var files = startPath.GetFiles();
            data.AddRange(files);
            foreach (var dir in startPath.GetDirectories())
            {
                GetFiles(data, dir);
            }
        }
    }
}
