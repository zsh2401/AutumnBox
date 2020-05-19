using AutumnBox.Core;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.IO;
using System.Linq;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(SingletonMode = true, Type = typeof(IStorage))]
    internal class StorageManager : IStorageManager
    {
        public IStorage Open(string id)
        {
            return new Storage(id);
        }
    }

    internal class Storage : IStorage
    {
        private const string CACHE_DIR = "cache";
        private const string FILES_DIR = "files";
        private const string JSON_EXT = ".ajson";
        private const string FILE_EXT = ".aextf";

        public DirectoryInfo CacheDirectory { get; private set; }
        private DirectoryInfo FilesDirectory { get; set; }
        public Storage(string id)
        {
            var baseApi = LakeProvider.Lake.Get<IBaseApi>();

            string cacheDirPath = Path.Combine(baseApi.TempDirectory.FullName, $"extcache_{id}");
            string filesDirPath = Path.Combine(baseApi.StorageDirectory.FullName, $"extfiles_{id}");

            CacheDirectory = new DirectoryInfo(cacheDirPath);
            FilesDirectory = new DirectoryInfo(filesDirPath);

            if (!CacheDirectory.Exists) CacheDirectory.Create();
            if (!FilesDirectory.Exists) FilesDirectory.Create();
        }

        public void ClearCache()
        {
            CacheDirectory.Delete(true);
            CacheDirectory.Create();
        }

        public void ClearFiles()
        {
            FileInfo[] files = FilesDirectory.GetFiles();
            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public void ClearJsonObjects()
        {
            var jsonFiles = from file in FilesDirectory.GetFiles($"*{JSON_EXT}")
                            where file.Extension == JSON_EXT
                            select file;

            foreach (var file in jsonFiles)
            {
                file.Delete();
            }
        }

        private string GenerateFileName(string fileId)
        {
            return fileId.ToString();
        }

        public void DeleteFile(string fileId)
        {
            var path = Path.Combine(FilesDirectory.FullName, GenerateFileName(fileId) + FILE_EXT);
            File.Delete(path);
        }

        public FileStream OpenFile(string fileId, bool createIfNotExist = true)
        {
            var path = Path.Combine(FilesDirectory.FullName, GenerateFileName(fileId) + FILE_EXT);
            FileMode mode = createIfNotExist ? FileMode.OpenOrCreate : FileMode.Open;
            var fs = new FileStream(path, mode, FileAccess.ReadWrite);
            return fs;
        }

        public TResult ReadJsonObject<TResult>(string jsonId)
        {
            try
            {
                using var fs = OpenFile(jsonId, false);
                using var sr = new StreamReader(fs);
                var json = sr.ReadToEnd();
                return JsonHelper.DeserializeObject<TResult>(json);
            }
            catch
            {
                return default;
            }
        }

        public void SaveJsonObject(string jsonId, object jsonObject)
        {
            using var fs = OpenFile(jsonId);
            using var sw = new StreamWriter(fs);
            var json = JsonHelper.SerializeObject(jsonObject);
            sw.Write(json);
            sw.Flush();
        }

        public void Restore()
        {
            ClearCache();
            ClearFiles();
            ClearJsonObjects();
        }

        public FileInfo WriteToFile(Stream srcSource, string fileId)
        {
            string path = Path.Combine(FilesDirectory.FullName, GenerateFileName(fileId) + FILE_EXT);
            using (var writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                srcSource.CopyTo(writer);
                writer.Flush();
            }
            FileInfo file = new FileInfo(path);
            return file;
        }
    }
}
