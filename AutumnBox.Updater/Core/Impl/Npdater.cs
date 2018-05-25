/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 17:53:10 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutumnBox.Updater.Core.Impl
{
    sealed class Npdater : IUpdater
    {
        private readonly IDiffer Differ  = new NDiffer();
        private readonly IUpdateInfoGetter InfoGetter { get; set; }
        private readonly IDownloader Downloader { get; set; }
        public void Start(IProgressWindow prgWin)
        {
            try
            {
                Flow(prgWin);
            }
            catch (Exception ex)
            {
                prgWin.SetTipColor(Colors.Red);
                prgWin.SetTip("下载失败" + ex.ToString());
            }
        }
        private void Flow(IProgressWindow prgWin)
        {
            prgWin.SetTip("获取信息中", 10);
            IUpdateInfo info = InfoGetter.Get();
            prgWin.SetTip("获取信息完毕,正在解析", 20);
            IEnumerable<IFile> needUpdateFile = Differ.Diff(info.Files, GetLocalFiles());
            prgWin.SetTip("正在下载", 30);
            Downloader.Download(needUpdateFile);
            prgWin.SetTip("下载完毕", 100);
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
                GetFiles(data, startPath);
            }
        }
    }
}
