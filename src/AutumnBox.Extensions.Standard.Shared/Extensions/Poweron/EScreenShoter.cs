/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;
using HandyControl.Controls;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Screen capture", "zh-cn:截图")]
    [ExtIcon("Icons.screenshotv2.png")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ClassText("error", "Can not get screenshot.Plz unlock device at least once or replug it.", "zh-cn:截图失败，请至少解锁一次手机或重新拔插设备")]
    internal class EScreenShoter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IDevice device, ILeafUI ui, IStorage storage,
            IAppManager app, ICommandExecutor executor)
        {
            using (ui)
            {
                //初始化LeafUI并展示
                executor.OutputReceived += (s, e) =>
                {
                    ui.Println(e.Text);
                };
                ui.Closing += (s, e) =>
                {
                    try { executor.Dispose(); } catch { }
                    return true;
                };

                ui.Show();

                try
                {
                    var screencap = new ScreenCap(device, executor, storage.CacheDirectory.FullName);
                    var file = screencap.Cap();
                    ui.Println(file.FullName);
                    ShowOnUI(app, file.FullName);
                    ui.Shutdown();
                }
                catch (CommandCancelledException)
                {
                    ui.Println("Cancelled");
                }
                catch (CommandErrorException e)
                {
                    ui.ShowMessage(this.RxGetClassText("error"));
                    ui.Println(e.ToString());
                    ui.Finish(StatusMessages.Failed);
                }
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

