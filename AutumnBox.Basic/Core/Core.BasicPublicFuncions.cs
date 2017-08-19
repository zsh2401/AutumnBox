using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Arg;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public void FlashCustomRecovery(string id, string file)
        {
            CustomRecoveryFlasher flasher = new CustomRecoveryFlasher();
            flasher.FlashFinish += this.FlashCustomRecoveryFinish;
            flasher.Run(new FileArgs { deviceID = id, files = new string[] { file } });
        }
        /// <summary>
        /// 向一个设备推送文件
        /// </summary>
        /// <param name="obj">这个参数必须为一个string列表,列表0是id,列表1是文件名</param>
        public Thread PushFileToSdcard(string id, string file)
        {
            FileSender fs = new FileSender();
            fs.sendAllFinish += this.SendFileFinish;
            return fs.Run(new FileArgs { deviceID = id, files = new string[] { file } });
        }
        /// <summary>
        /// 重启设备
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="option">重启到的状态,不填默认重启到系统/param>
        public void Reboot(string id, RebootOptions option = RebootOptions.System)
        {
            RebootOperator ro = new RebootOperator();
            ro.RebootFinish += this.RebootFinish;
            ro.Run(new RebootArgs { deviceID = id, nowStatus = DevicesTools.GetDeviceStatus(id), rebootOption = option }); ;
        }
        public void KillAdb()
        {
            Adb.KillAdb();
        }
    }
}
