/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:09:45 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class EmbeddedFileManagerImpl : IEmbeddedFileManager
    {
        private readonly Context ctx;
        public EmbeddedFileManagerImpl(Context ctx)
        {
            this.ctx = ctx;
        }

        public Stream GetStream(string innerResPath)
        {
            string fullPath = ctx.GetType().Namespace + "." + innerResPath;
            return ctx.GetType().Assembly
                .GetManifestResourceStream(fullPath);
        }
    }
}
