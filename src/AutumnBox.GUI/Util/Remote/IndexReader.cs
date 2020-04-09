/*

* ==============================================================================
*
* Filename: IndexReader
* Description: 
*
* Version: 1.0
* Created: 2020/3/16 23:20:56
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Remote
{
    class APIIndex
    {
        [JsonProperty("motd")]
        public string MotdUrl { get; }
    }
    class IndexReader
    {
        private const string KEY_INDEX_URL = "URL_API_INDEX";
        public async Task<APIIndex> Do()
        {
            var indexUrl = (string)App.Current.Resources[KEY_INDEX_URL];
            return await new NetReader()
                .Read(indexUrl)
                .ContinueWith(task => task.Result.ToJson<APIIndex>());
        }
    }
}
