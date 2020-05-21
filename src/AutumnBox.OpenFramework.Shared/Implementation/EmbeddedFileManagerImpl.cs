/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:09:45 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Open;
using System.IO;
using System.Reflection;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(IEmbeddedFileManager))]
    class EmbeddedFileManagerImpl : IEmbeddedFileManager
    {
        private class EmbeddedFileImpl : IEmbeddedFile
        {
            private readonly Assembly assembly;
            public readonly string path;
            public EmbeddedFileImpl(Assembly assembly, string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new System.ArgumentException("message", nameof(path));
                }
                this.assembly = assembly ?? throw new System.ArgumentNullException(nameof(assembly));
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
                string fullPath = assembly.GetName().Name + "." + path;
                var stream = assembly
                    .GetManifestResourceStream(fullPath);
                return stream;
            }
            public void WriteTo(FileStream fs)
            {
                CopyTo(fs);
            }
        }
        public IEmbeddedFile Get(Assembly assembly, string innerResPath)
        {
            return new EmbeddedFileImpl(assembly, innerResPath);
        }
    }
}
