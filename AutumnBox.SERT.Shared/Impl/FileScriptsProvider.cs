using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.SERT.Impl
{
    public class FileScriptsProvider : IRawScriptsProvider
    {
        private readonly DirectoryInfo dir;

        public FileScriptsProvider(DirectoryInfo dir, bool scanImmediately = true)
        {
            if (dir is null)
            {
                throw new ArgumentNullException(nameof(dir));
            }

            if (scanImmediately)
            {
                Scan();
            }

            this.dir = dir;
        }

        public Dictionary<int, string> Scripts => throw new NotImplementedException();

        public void Scan()
        {
            Scripts.Clear();
            var jsFiles = dir.GetFiles("*.js");
            foreach (var file in jsFiles)
            {
                using (var reader = file.OpenText())
                {
                    var content = reader.ReadToEnd();
                    Scripts.Add(file.Name.GetHashCode(), content);
                }
            }
        }
    }
}
