using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.LeafExtension.Internal.Impl
{
    internal class LeafEmb : IEmbeddedFileManager
    {
        private class LeafEmbFile : IEmbeddedFile
        {
            private readonly Assembly assembly;
            public readonly string path;
            public LeafEmbFile(Assembly assembly, string path)
            {
                this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
                this.path = path ?? throw new ArgumentNullException(nameof(path));
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
        private readonly Assembly assembly;
        public LeafEmb(Assembly assembly)
        {
            this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }
        public IEmbeddedFile Get(string innerResPath)
        {
            return new LeafEmbFile(assembly, innerResPath);
        }
    }
}
