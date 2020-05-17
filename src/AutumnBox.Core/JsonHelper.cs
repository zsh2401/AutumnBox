/*

* ==============================================================================
*
* Filename: JsonHelper
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 21:03:40
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
#if USE_NT_JSON
using Newtonsoft.Json;
#endif

namespace AutumnBox.Core
{
    /// <summary>
    /// 具体的Json实现方案
    /// </summary>
    internal static class JsonHelper
    {
        public static T DeserializeObject<T>(string json)
        {
#if USE_NT_JSON
            return JsonConvert.DeserializeObject<T>(json);
#endif
        }
        public static string SerializeObject(object jsonObject)
        {
#if USE_NT_JSON
            return JsonConvert.SerializeObject(jsonObject);
#endif
        }
    }
}
