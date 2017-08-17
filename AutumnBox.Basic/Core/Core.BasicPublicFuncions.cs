using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*此文件中的方法,是已经通过实验,确定可以正常工作的功能*/
namespace AutumnBox.Basic
{
    public partial class Core
    {
        /// <summary>
        /// 向一个处于fastboot模式的设备刷入电脑上指定的recovery镜像
        /// </summary>
        /// <param name="obj">这个参数必须为一个string列表,列表0是id,列表1是文件名</param>
        public void FlashCustomRecovery(object obj)
        {
            string[] args = (string[])obj;
            FlashRecoveryStart?.Invoke(args);
            fe($" -s {args[0]} flash recovery \"{args[1]}\"");
            fe($" -s  {args[0]}  boot {args[1]}");
            FlashRecoveryFinish?.Invoke(new OutputData());
        }
        /// <summary>
        /// 向一个设备推送文件
        /// </summary>
        /// <param name="obj">这个参数必须为一个string列表,列表0是id,列表1是文件名</param>
        public void PushFileToSdcard(object obj)
        {
            string[] args = (string[])obj;
            PushStart?.Invoke(obj);
            OutputData output = ae($" -s {args[0]} push \"{args[1]}\" /sdcard/");
            PushFinish?.Invoke(output);
        }
        /// <summary>
        /// 重启设备
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="option">重启到的状态,不填默认重启到系统/param>
        public void Reboot(string id, RebootOptions option = RebootOptions.System)
        {
            DeviceStatus ds = GetDeviceStatus(id);
            OutputData outputData;
            switch (option)
            {
                case RebootOptions.Bootloader:
                    if (ds == DeviceStatus.FASTBOOT) outputData = fe($" -s {id} reboot-bootloader");
                    else outputData = ae($" -s {id} reboot-bootloader");
                    break;
                case RebootOptions.Recovery:
                    outputData = ae($" -s {id} reboot recovery");
                    break;
                default:
                    if (ds == DeviceStatus.FASTBOOT) outputData = fe($" -s {id} reboot");
                    else outputData = ae($" -s {id} reboot");
                    break;
            }
            RebootFinish?.Invoke(outputData);
        }
    }
}
