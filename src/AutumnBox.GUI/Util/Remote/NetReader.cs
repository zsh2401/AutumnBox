/*

* ==============================================================================
*
* Filename: NetReader
* Description: 
*
* Version: 1.0
* Created: 2020/3/16 23:23:15
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Remote
{
    class NetReader
    {
        private static readonly WebClient webClient =
            new WebClient() { Encoding = Encoding.UTF8 };
        private string text;
        public async Task<NetReader> Read(string url)
        {
            text = await Task.Run(() => webClient.DownloadString(new Uri(url)));
            return this;
        }
        public TOBJ ToJson<TOBJ>()
        {
            var obj = JsonConvert.DeserializeObject<TOBJ>(text);
            var rawJsonProperty = obj.GetType().GetProperty("RawJson");
            if (rawJsonProperty != null
                && rawJsonProperty.GetSetMethod() != null
                && rawJsonProperty.PropertyType == typeof(string))
            {
                rawJsonProperty.GetSetMethod().Invoke(obj, new object[] { text });
            }
            return obj;
        }
        public string ToText()
        {
            return text;
        }
    }
}
