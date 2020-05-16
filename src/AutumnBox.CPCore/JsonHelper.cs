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

namespace AutumnBox.Core
{
    /// <summary>
    /// 具体的Json实现方案
    /// </summary>
    internal static class JsonHelper
    {
        public static T DeserializeObject<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
        public static string SerializeObject(object jsonObject)
        {
            return System.Text.Json.JsonSerializer.Serialize(jsonObject);
        }
    }
}
