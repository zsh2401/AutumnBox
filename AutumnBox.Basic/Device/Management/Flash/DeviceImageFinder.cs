/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 11:22:29
** filename: DeviceImageOperator.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using System;
using System.Linq;

namespace AutumnBox.Basic.Device.Management.Flash
{

    /// <summary>
    /// 设备镜像路径寻找器,由于安卓碎片化严重,不保证能完美运行,如果有特殊需求请另行实现
    /// </summary>
    public sealed class DeviceImageFinder : IDisposable,ISetableShell
    {
        private readonly DeviceSerialNumber serial;
        /// <summary>
        /// 使用的android shell类
        /// </summary>
        public AndroidShell ShellAsSu
        {
            private get
            {
                if (_shell == null)
                {
                    _shell = new AndroidShell(serial);
                    _shell.Connect();
                    _shell.Switch2Su();
                }
                return _shell;
            }
            set
            {
                _shell = value;
            }
        }
        private AndroidShell _shell;
        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="serial"></param>
        public DeviceImageFinder(DeviceSerialNumber serial)
        {
            this.serial = serial;
        }
        /// <summary>
        /// 获取目标设备的指定image路径
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public static string PathOf(DeviceSerialNumber serial, DeviceImage imageType)
        {
            using (DeviceImageFinder _o = new DeviceImageFinder(serial))
            {
                return _o.PathOf(imageType);
            }
        }
       /// <summary>
       /// 获取image的路径
       /// </summary>
       /// <param name="imageType"></param>
       /// <returns></returns>
        public string PathOf(DeviceImage imageType)
        {
            return Find1(imageType) ?? Find2(imageType);
        }

        private string Find1(DeviceImage image)
        {
            var exeResult = ShellAsSu.SafetyInput($"find /dev/ -name {image.ToString().ToLower()}");
            exeResult.PrintOnLog(this);
            if (exeResult.GetExitCode() == (int)LinuxReturnCode.KeyHasExpired)
            {
                return null;//无法使用find命令,当场返回!
            }
            else
            {
                var result = from r in exeResult.LineAll
                             where PathIsRight(r)
                             select r;
                return result.First();
            }
        }
        private string Find2(DeviceImage image)
        {
            string maybePath1 = $"/dev/block/platform/*/by-name/{image.ToString().ToLower()}";
            string maybePath2 = $"/dev/block/platform/soc*/*/by-name/{image.ToString().ToLower()}";

            var exeResult = ShellAsSu.SafetyInput($"ls -l {maybePath1}");
            if (exeResult.IsSuccessful)
            {
                return maybePath1;
            }
            exeResult = ShellAsSu.SafetyInput($"ls -l {maybePath2}");
            if (exeResult.IsSuccessful)
            {
                return maybePath2;
            }
            return null;
        }

        private bool PathIsRight(string path)
        {
            return ShellAsSu.SafetyInput($"ls -l {path}").IsSuccessful;
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            this.ShellAsSu?.Dispose();
        }
    }
}
