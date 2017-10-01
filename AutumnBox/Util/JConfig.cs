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
    public class JConfig
    {
        private static class Default
        {
            public static readonly JObject defaultJ = Init();
            private static JObject Init()
            {
                JObject j = new JObject();
                j.Add("SkipVersion", SkipVersion);
                j.Add("IsFristLaunch", IsFirstLaunch);
                return j;
            }
            public static readonly string SkipVersion = String.Empty;
            public static readonly bool IsFirstLaunch = true;
        }
        private static readonly string KEY = "1210626737";
        private static readonly string CONFIG_FILE = "alo.j";
        public JObject SourceData { get; private set; }
        public JConfig()
        {
            Read();
        }
        public void Save()
        {
            WriteToFile(CONFIG_FILE, this.SourceData.ToString());
        }
        private void Read()
        {
            string content = String.Empty;
            if (!File.Exists(CONFIG_FILE))
            {
                WriteToFile(CONFIG_FILE, Default.defaultJ.ToString());
            }
            ReadForFile(CONFIG_FILE, ref content);
            SourceData = JObject.Parse(content);
        }
        public JToken this[string key] {
            get { return SourceData[key]; }
            set { SourceData[key] = value; }
        }
        /***************STATIC***************/
        private static bool IsEn()
        {
            try
            {
                string content = String.Empty;
                //ReadForFile(CONFIG_FILE, ref content);

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
            WriteToFile(CONFIG_FILE, Default.defaultJ.ToString());
        }
        private static bool ReadForFile(string path, ref string content)
        {
#if DEBUG
            if (IsEn()) {
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
            FileStream fs = new FileStream(CONFIG_FILE, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
#if DEBUG
            sw.Write(content);
#else
            sw.Write(En(KEY, content));
            Log.d("JCONFIG", En(KEY, content));
#endif
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
