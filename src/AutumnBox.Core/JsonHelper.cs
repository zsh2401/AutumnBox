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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Core
{
    /// <summary>
    /// 具体的Json实现方案
    /// </summary>
    internal static class JsonHelper
    {
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static string SerializeObject(object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject);
        }
    }
}
