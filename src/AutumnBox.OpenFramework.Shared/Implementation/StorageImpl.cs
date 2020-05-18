using AutumnBox.Core;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Open;
using System.IO;
namespace AutumnBox.OpenFramework.Implementation
{
    [Component(SingletonMode = true, Type = typeof(IStorageManager))]
    internal class StorageManager : IStorageManager
    {
        public IStorage Open(string id)
        {
            return new Storage(id);
        }
    }

    internal class Storage : IStorage
    {
        private string storageId;
        private const string CACHE_DIR = "cache";
        private const string FILES_DIR = "files";
        private const string JSON_EXT = ".ajson";
        private const string FILE_EXT = ".aextf";

        public DirectoryInfo CacheDirectory { get; private set; }
        private DirectoryInfo ChiefDirectory { get; set; }
        private DirectoryInfo FilesDirectory { get; set; }
        public Storage(string id)
        {
            storageId = id.GetHashCode().ToString();

            /*初始化Chief Directory*/
            string chiefDirName = storageId.GetHashCode().ToString();
            string chiefPath = Path.Combine(BuildInfo.DEFAULT_EXTENSION_PATH, chiefDirName);
            ChiefDirectory = new DirectoryInfo(chiefPath);
            if (!ChiefDirectory.Exists) ChiefDirectory.Create();

            /*初始化子文件夹*/
            CacheDirectory = InitChildDirectory(CACHE_DIR);
            FilesDirectory = InitChildDirectory(FILES_DIR);
        }
        private DirectoryInfo InitChildDirectory(string dirName)
        {
            string path = Path.Combine(ChiefDirectory.FullName, dirName);
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists) dir.Create();
            return dir;
        }

        public void ClearCache()
        {
            CacheDirectory.Delete(true);
            InitChildDirectory(CACHE_DIR);
        }

        public void ClearFiles()
        {
            FileInfo[] files = FilesDirectory.GetFiles($"*{FILE_EXT}");
            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public void ClearJsonObjects()
        {
            FileInfo[] files = FilesDirectory.GetFiles($"*{JSON_EXT}");
            foreach (var file in files)
            {
                file.Delete();
            }
        }

        private string GenerateFileName(string fileId)
        {
            return fileId.GetHashCode().ToString();
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
        }

        public void Restore()
        {
            ClearCache();
            ClearFiles();
            ClearJsonObjects();
        }

        public FileInfo WriteToFile(Stream srcSource, string fileId = null)
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
