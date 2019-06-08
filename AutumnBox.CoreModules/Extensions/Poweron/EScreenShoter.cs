/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.Logging;
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
            IAppManager app, ICommandExecutor executor)
        {
            using (ui)
            {
                //初始化LeafUI并展示
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                executor.OutputReceived += (s, e) =>
                {
                    ui.WriteOutput(e.Text);
                    SLogger<EScreenShoter>.Info(e.Text);
                };
                ui.Closing += (s, e) =>
                {
                    executor.Dispose();
                    return true;
                };

                ui.Show();

                var screencap = new ScreenCap(device, executor, storageManager.CacheDirectory.FullName);
                var file = screencap.Cap();

                ui.WriteLine(file.FullName);
                ShowOnUI(app, file.FullName);
                //显示出来了,进度窗也没啥用了,直接关闭
                //ui.Finish();
                ui.Shutdown();
            }
        }
        private static void ShowOnUI(IAppManager app, string pngFile)
        {
            app.RunOnUIThread(() =>
            {
                //获取秋之盒主窗口
                var window = (Window)app.GetMainWindow();
                //构建一个HandyControl的图片浏览器
                var imgWindow = new ImageBrowser(pngFile);
                //当主窗口关闭时,关闭图片浏览器窗口
                //不使用Window.Owner是为了避免图片窗始终在秋之盒主窗体上层
                window.Closing += (s, e) =>
                {
                    imgWindow.Close();
                };
                //显示浏览器窗口
                imgWindow.Show();
            });
        }
    }
}

