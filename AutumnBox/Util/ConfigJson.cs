using AutumnBox.Basic.Util;
using AutumnBox.Debug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    public class ConfigJson
    {
        public static readonly JObject defaultJ = Init();
        private static JObject Init()
        {
            JObject j = new JObject
                {
                    { "Lang","zh-CN"},
                    { "IsFirstLaunch",true },
                    { "SkipVersion", "xx" }
                };
            return j;
        }
        private static readonly string KEY = "1210626737";
        private static readonly string CONFIG_FILE = "alo.j";
        public JObject SourceData { get; private set; }
        public ConfigJson()
        {
            try
            {
                Read();
            }
            catch (FileNotFoundException)
            {
                SourceData = defaultJ;
                Save();
            }
        }
        public void Save()
        {
            Log.d("ConfigJson Saving", this.SourceData.ToString());
            WriteToFile(CONFIG_FILE, this.SourceData.ToString());
        }
        private void Read()
        {
            string content = String.Empty;
            ReadForFile(CONFIG_FILE, ref content);
            SourceData = JObject.Parse(content);
        }
        public JToken this[string key]
        {
            get { return SourceData[key]; }
            set { SourceData[key] = value; }
        }
        /***************STATIC***************/
        private static bool IsEn()
        {
            try
            {
                string content = File.ReadAllText(CONFIG_FILE);
                JObject.Parse(content);
                return false;
            }
            catch (JsonReaderException)
            {
                return true;
            }
        }
        private static string En(string Key, string content)
        {
            //http://www.jb51.net/article/58442.htm
            char[] data = content.ToCharArray();
            char[] key = Key.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }
            return new string(data);
        }
        private static string De(string Key, string ciphertext)
        {
            char[] data = ciphertext.ToCharArray();
            char[] key = Key.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= key[i % key.Length];
            }
            return new string(data);
        }
        private static void InitFile()
        {
            WriteToFile(CONFIG_FILE, defaultJ.ToString());
        }
        private static bool ReadForFile(string path, ref string content)
        {
#if DEBUG
            if (IsEn())
            {
                InitFile();
            }
#else
            if (!IsEn()) {
                InitFile();
            }
#endif
            try
            {
                FileStream fs = new FileStream(CONFIG_FILE, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                content = sr.ReadToEnd();
#if! DEBUG
                content = De(KEY, content);
#endif
                fs.Close();
                sr.Close();
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
        private static void WriteToFile(string path, string content)
        {
            Log.d("Saving", content);
#if DEBUG
            File.WriteAllText(CONFIG_FILE, content);
#else
            File.WriteAllText(CONFIG_FILE,En(KEY, content));
#endif
        }
    }
}
