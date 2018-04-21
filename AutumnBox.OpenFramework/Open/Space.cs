/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/18 22:45:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using System.IO;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 空间管理器
    /// </summary>
    public static class Space
    {
        /// <summary>
        /// 获取拓展模块所在位置路径
        /// </summary>
        public static string ExtensionsPath
        {
            get
            {
                return ExtensionManager.ExtensionsPath_Internal;
            }
        }
        /// <summary>
        /// 根据Context获取临时文件夹
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string GetTempDir(Context ctx)
        {
            var path = Path.Combine(ExtensionManager.ExtensionsPath_Internal, ctx.GetType().Name);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        /// <summary>
        /// 清理对应Context的临时文件夹
        /// </summary>
        /// <param name="ctx"></param>
        public static void CleanTempDir(Context ctx)
        {
            DirectoryInfo dir = new DirectoryInfo(GetTempDir(ctx));
            dir.Delete(true);
        }
    }
}
