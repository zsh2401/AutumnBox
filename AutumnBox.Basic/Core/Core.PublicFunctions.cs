#define inAutumnBox
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System.Collections;
using System.Diagnostics;
using System.Threading;

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
        /// <summary>
        /// 重新给小米手机上锁
        /// </summary>
        /// <param name="arg">设备id</param>
        public void RelockMi(object args)
        {
            string id = args.ToString();
            OutputData o = fe($" -s {args.ToString()} oem lock");
            Reboot(args.ToString(), RebootOptions.System);
            Thread.Sleep(2000);
            Reboot(id, RebootOptions.System);
            RelockMiFinish?.Invoke(o);
        }
        /// <summary>
        /// 解锁小米手机系统分区,获取完整的root权限
        /// </summary>
        /// <param name="arg">设备id</param>
        public void UnlockMiSystem(object args)
        {
            string id = args.ToString();
            ae($" -s {id} root");
            OutputData o = ae($" -s {id} disable-verity");
            UnlockMiSystemFinish?.Invoke(o);
            Thread.Sleep(2000);
            Reboot(id, RebootOptions.System);
        }
        /// <summary>
        /// 进行sideload刷机
        /// </summary>
        /// <param name="arg">一个列表,列表0元素为设备id,后面的所有元素为要刷入的文件</param>
        public void Sideload(object args)
        {
            string[] a = (string[])args;
#if inAutumnBox
            Process.Start(files["sideloadbat"].ToString(), $"{a[0]} {a[1]}");
            SideloadFinish?.Invoke(new OutputData());
#endif
            
        }
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
        /// <summary>
        /// 删除设备的屏幕锁
        /// </summary>
        /// <param name="id">设备id</param>
        public void UnlockScreenLock(object id)
        {
            ae($"-s{id.ToString()} root");
            ae($"-s{id.ToString()} shell \"rm -rf /data/system/*password.key\"");
            ae($"-s{id.ToString()} shell \"rm -rf /data/system/*gesture.key\"");
            UnlockScreenLockFinish?.Invoke(new OutputData());
        }
        /// <summary>
        /// 关机
        /// </summary>
        /// <param name="id"></param>
        public void Shutdown(object id)
        {
           //TODO
        }
    }
}
