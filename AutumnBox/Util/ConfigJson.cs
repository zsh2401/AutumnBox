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
                    { "SkipVersion", "0.10.0" }
                };
            return j;
        }
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
            catch (Exception e) {
                Log.d("Catcho e Exp",e.ToString());
            }
        }
        public void Save()
        {
            //Log.d("ConfigJson Saving", this.SourceData.ToString());
            File.WriteAllText(CONFIG_FILE, this.SourceData.ToString()); 
        }
        private void Read()
        {
            FileStream fs = new FileStream(CONFIG_FILE, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string  content = sr.ReadToEnd();
            fs.Close();
            sr.Close();
            SourceData = JObject.Parse(content);
        }
        public JToken this[string key]
        {
            get { return SourceData[key]; }
            set { SourceData[key] = value; }
        }
        /***************STATIC***************/
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

    }
}
