/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/11 12:34:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 内嵌资源提取器
    /// </summary>
    public class Extractor
    {
        private readonly Context ctx;
        private readonly Type ctxType;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="ctx">对应上下文</param>
        public Extractor(Context ctx)
        {
            this.ctx = ctx;
            ctxType = ctx.GetType();
        }
        /// <summary>
        /// 将内嵌的资源提取到临时文件夹下的目标路径
        /// </summary>
        /// <param name="resPath"></param>
        /// <param name="targetPath"></param>
        public void ExtractToTmp(string resPath, string targetPath)
        {
            Extract(resPath, Path.Combine(Space.GetTempDir(ctx), targetPath));
        }
        /// <summary>
        /// 提取到指定目录
        /// </summary>
        /// <param name="resPath"></param>
        /// <param name="targetPath"></param>
        public void Extract(string resPath, string targetPath)
        {
            Check(targetPath);
            string fullResPath = ctxType.Namespace + "." + resPath;
            using (var resStream = ctxType.Assembly.GetManifestResourceStream(fullResPath))
            {
                using (FileStream fs =
                    new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    long totalSize = 0;
                    int crt = 0;
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        crt = resStream.Read(buffer, 0, buffer.Length);
                        fs.Write(buffer, 0, crt);
                        totalSize += crt;
                        if (crt == 0) break;
                    }
                }
            }
        }
        /// <summary>
        /// 检查目录是否正确
        /// </summary>
        /// <param name="filePath"></param>
        private static void Check(string filePath) {
            var file = Path.GetFileName(filePath);
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }
    }
}
