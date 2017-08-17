#define inAutumnBox
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System.Collections;
using System.Diagnostics;
using System.Threading;
/*
 此文件中的方法为可供外界调用的实验性方法
     */
namespace AutumnBox.Basic
{
    public partial class Core
    {
       
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
        /// <summary>
        /// 杀死ADB服务
        /// </summary>
        public void KillAdb()
        {
            ae("kill-server");
        }
        /// <summary>
        /// 激活黑域服务
        /// </summary>
        /// <param name="id">设备id</param>
        public void ActivateBrevent(object id)
        {
            ae($"-s {id} shell 'sh /data/data/me.piebridge.brevent/brevent.sh'");
        }
    }
}
