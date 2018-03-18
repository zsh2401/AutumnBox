/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/18 22:45:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using System.IO;

namespace AutumnBox.OpenFramework.Open
{
    public static class Space
    {
        /// <summary>
        /// 根据Context获取临时文件夹
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string GetTempDir(Context ctx)
        {
            var path = Path.Combine(ExtensionManager.ExtensionsPath, ctx.GetType().Name);
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
