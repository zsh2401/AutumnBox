/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using HandyControl.Controls;
using System;
using System.IO;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Capture the screen and save it to pc", "zh-cn:截图并保存到电脑")]
    [ExtIcon("Icons.screenshot.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EScreenShoter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IDevice device, ILeafUI ui, IStorageManager storageManager,
            IAppManager app, Basic.Calling.ICommandExecutor executor)
        {
            using (ui)
            {
                //初始化LeafUI并展示
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                executor.OutputReceived += (s, e) => ui.WriteOutput(e.Text);
                ui.Closing += (s, e) =>
                {
                    executor.Dispose();
                    return true;
                };

                ui.Show();


                //生成一个在设备上的临时文件名
                var tmpPath = GenerateTmpPath();
                //点亮屏幕
                new KeyInputer(device, executor).RaiseKeyEvent(AndroidKeyCode.WakeUp);
                //进行截图,并保存到那个临时文件
                var capResult = executor.AdbShell(device, $"screencap -p {tmpPath}");
                //如果成功了
                if (capResult.ExitCode == 0)
                {
                    //获取一个PC上的临时文件名
                    var saveTarget = GetTempFile(storageManager);
                    //拉取手机上的临时文件,并保存到PC
                    executor.Adb(device, $"pull {tmpPath} \"{saveTarget}\"");
                    //删除手机上的临时文件
                    executor.AdbShell(device, $"rm -f {tmpPath}");
                    //在UI线程运行
                    ui.RunOnUIThread(() =>
                    {
                        //获取秋之盒主窗口
                        var window = (Window)app.GetMainWindow();
                        //构建一个HandyControl的图片浏览器
                        var imgWindow = new ImageBrowser(saveTarget.FullName);
                        //当主窗口关闭时,关闭图片浏览器窗口
                        //不使用Window.Owner是为了避免图片窗始终在秋之盒主窗体上层
                        window.Closing += (s, e) =>
                        {
                            imgWindow.Close();
                        };
                        //显示浏览器窗口
                        imgWindow.Show();
                    });
                    //显示出来了,进度窗也没啥用了,直接关闭
                    ui.EShutdown();
                }
                //有错误,进行错误显示
                ui.Finish(capResult.ExitCode);
            }
        }
        /// <summary>
        /// 生成PC上的临时截图文件名
        /// </summary>
        /// <param name="storageManager"></param>
        /// <returns></returns>
        private FileInfo GetTempFile(IStorageManager storageManager)
        {
            string randomFileName = $"atmb_screenshot_{Guid.NewGuid().ToString()}.png";
            var path = Path.Combine(storageManager.CacheDirectory.FullName, randomFileName);
            return new FileInfo(path);
        }
        //private FileInfo GetSaveTarget(ILeafUI ui)
        //{
        //    DialogResult dialogResult = DialogResult.No;
        //    FileInfo path = null;
        //    string saveDir = null;
        //    ui.RunOnUIThread(() =>
        //    {
        //        FolderBrowserDialog fbd = new FolderBrowserDialog();
        //        dialogResult = fbd.ShowDialog();
        //        saveDir = fbd.SelectedPath;
        //    });
        //    if (dialogResult == DialogResult.OK)
        //    {
        //        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //        path = new FileInfo(Path.Combine(saveDir, fileName + ".png"));
        //        return path;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        /// <summary>
        /// 生成手机上的临时截图文件名
        /// </summary>
        /// <returns></returns>
        private string GenerateTmpPath()
        {
            return $"/sdcard/atmb_screenshot_{Guid.NewGuid().ToString()}.png";
        }
    }
}
