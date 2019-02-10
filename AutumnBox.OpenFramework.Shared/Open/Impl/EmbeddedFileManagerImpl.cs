/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:09:45 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using System.IO;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class EmbeddedFileManagerImpl : IEmbeddedFileManager
    {
        private class EmbeddedFileImpl : IEmbeddedFile
        {
            public readonly Context ctx;
            public readonly string path;
            public EmbeddedFileImpl(Context ctx, string path)
            {
                this.ctx = ctx;
                this.path = path;
            }
            public void CopyTo(Stream targetStream)
            {
                using (var stream = GetStream())
                {
                    stream.CopyTo(targetStream);
                }
            }
            public void ExtractTo(FileInfo targetFile)
            {
                using (FileStream fs = new FileStream(targetFile.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    WriteTo(fs);
                }
            }
            public Stream GetStream()
            {
                string fullPath = ctx.GetType().Assembly.GetName().Name + "." + path;
                var stream = ctx.GetType().Assembly
                    .GetManifestResourceStream(fullPath);
                return stream;
            }
            public void WriteTo(FileStream fs)
            {
                CopyTo(fs);
            }
        }
        private readonly Context ctx;
        public EmbeddedFileManagerImpl(Context ctx)
        {
            this.ctx = ctx;
        }
        public IEmbeddedFile Get(string innerResPath)
        {
            return new EmbeddedFileImpl(ctx, innerResPath);
        }
    }
}
