/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 1:45:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;
using Microsoft.Win32;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("刷入REC", Lang = "en-US")]
    [ExtName("Flash recovery.img",Lang ="en-US")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Fastboot)]
    public class EFlashRecovery : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = "选择一个文件";
            fileDialog.Filter = "镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var flasher = new RecoveryFlasher();
                flasher.Init(new RecoveryFlasherArgs()
                {
                    DevBasicInfo = TargetDevice,
                    RecoveryFilePath = fileDialog.FileName,
                });
                flasher.OutputReceived += (s, e) =>
                {
                    WriteLine(e.Text);
                };
                var result = flasher.Run();
                if (result.ResultType == Basic.FlowFramework.ResultType.Successful)
                {
                    return OK;
                }
                else
                {
                    return ERR;
                };
            }
            else
            {
                return ERR;
            }
        }
    }
}
