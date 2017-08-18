#define inAutumnBox
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Arg;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Other;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
/*此文件中的方法为可供外部调用的实验性方法*/
namespace AutumnBox.Basic
{
    public partial class Core
    {

        /// <summary>
        /// 重新给小米手机上锁
        /// </summary>
        /// <param name="arg">设备id</param>
        public void RelockMi(string id)
        {
            XiaomiBootloaderRelocker relocker = new XiaomiBootloaderRelocker();
            relocker.RelockFinish += this.XiaomiBootloaderRelockFinish;
            relocker.Run(new Args {deviceID = id});
            //string id = args.ToString();
            //OutputData o = fe($" -s {args.ToString()} oem lock");
            //Reboot(args.ToString(), RebootOptions.System);
            //Thread.Sleep(2000);
            //Reboot(id, RebootOptions.System);
            //RelockMiFinish?.Invoke(o);
        }
        /// <summary>
        /// 解锁小米手机系统分区,获取完整的root权限
        /// </summary>
        /// <param name="arg">设备id</param>
        public void UnlockMiSystem(string id)
        {
            XiaomiSystemUnlocker unlocker = new XiaomiSystemUnlocker();
            unlocker.UnlockFinish += this.XiaomiSystemUnlockFinish;
            unlocker.Run(new Args { deviceID = id });
            //string id = args.ToString();
            //ae($" -s {id} root");
            //OutputData o = ae($" -s {id} disable-verity");
            //UnlockMiSystemFinish?.Invoke(o);
            //Thread.Sleep(2000);
            //Reboot(id, RebootOptions.System);
        }
        /// <summary>
        /// 进行sideload刷机
        /// </summary>
        /// <param name="arg">一个列表,列表0元素为设备id,后面的所有元素为要刷入的文件</param>
        public void Sideload(object args)
        {
            //            string[] a = (string[])args;
            //#if inAutumnBox
            //            Process.Start(files["sideloadbat"].ToString(), $"{a[0]} {a[1]}");
            //            SideloadFinish?.Invoke(new OutputData());
            //#endif
        }

        [Obsolete("Please use AutumnBox.Basic.DevicesTools.GetDeviceInfo()")]
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeviceInfo GetDeviceInfo(string id)
        {
            Hashtable ht = GetBuildInfo(id);
            return new DeviceInfo
            {
                brand = ht["brand"].ToString(),
                code = ht["name"].ToString(),
                androidVersion = ht["androidVersion"].ToString(),
                model = ht["model"].ToString(),
                deviceStatus = GetDeviceStatus(id),
                id = id
            };
        }
        [Obsolete("Please use AutumnBox.Basic.DevicesTools.GetDeviceStatus()")]
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeviceStatus GetDeviceStatus(string id)
        {
            switch ((at.GetDevices() + ft.GetDevices())[id])
            {
                case "device":
                    return DeviceStatus.RUNNING;
                case "recovery":
                    return DeviceStatus.RECOVERY;
                case "fastboot":
                    return DeviceStatus.FASTBOOT;
                case "sideload":
                    return DeviceStatus.SIDELOAD;
                default:
                    return DeviceStatus.NO_DEVICE;
            }
        }
    }
}
