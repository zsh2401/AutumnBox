/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 11:22:29
** filename: DeviceImageOperator.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Util;
using System.Linq;

namespace AutumnBox.Basic.Device.Management.Flash
{

    /// <summary>
    /// 设备镜像路径寻找器,由于安卓碎片化严重,不保证能完美运行,如果有特殊需求请另行实现
    /// </summary>
    public sealed class DeviceImageFinder : DeviceCommander
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="device"></param>
        public DeviceImageFinder(IDevice device) : base(device)
        {
        }

        /// <summary>
        /// 获取目标设备的指定image路径
        /// </summary>
        /// <param name="device"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public static string PathOf(IDevice device, DeviceImage imageType)
        {
            return new DeviceImageFinder(device).PathOf(imageType);
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
            var exeResult = Device.Su($"find /dev/ -name {image.ToString().ToLower()}");
            if (exeResult.Item2 == (int)LinuxReturnCode.KeyHasExpired)
            {
                return null;//无法使用find命令,当场返回!
            }
            else
            {
                var result = from r in exeResult.Item1.LineAll
                             where PathIsRight(r)
                             select r;
                return result.First();
            }
        }
        private string Find2(DeviceImage image)
        {
            string maybePath1 = $"/dev/block/platform/*/by-name/{image.ToString().ToLower()}";
            string maybePath2 = $"/dev/block/platform/soc*/*/by-name/{image.ToString().ToLower()}";

            var exeResult = Device.Su($"ls -l {maybePath1}");
            if (exeResult.Item2 == (int)LinuxReturnCode.None)
            {
                return maybePath1;
            }
            exeResult = Device.Su($"ls -l {maybePath2}");
            if (exeResult.Item2 == (int)LinuxReturnCode.None)
            {
                return maybePath2;
            }
            return null;
        }

        private bool PathIsRight(string path)
        {
            return Device.Su($"ls -l {path}").Item2 == (int)LinuxReturnCode.None;
        }
    }
}
