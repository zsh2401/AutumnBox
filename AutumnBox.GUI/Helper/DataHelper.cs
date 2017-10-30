/* =============================================================================*\
*
* Filename: DataHelper
* Description: 
*
* Version: 1.0
* Created: 2017/10/29 18:10:37 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Shared.CstmDebug;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.GUI.Helper
{
    [LogProperty(TAG = "Data Helper")]
    public class DataHelperLogSender { }
    public static class DataHelper
    {
        private static DataHelperLogSender sender = new DataHelperLogSender();
        public static string JsonPropertyNameOf(object owner, string propName)
        {
            var props = owner.GetType().GetProperties();
            Logger.D( $" {owner.ToString()}'s {propName}");
            foreach (var _prop in props)
            {
                if (!(propName == _prop.Name)) continue;
                if (!_prop.IsDefined(typeof(JsonPropertyAttribute), true)) continue;
                string jsonPropName = ((JsonPropertyAttribute)Attribute.GetCustomAttribute(_prop, typeof(JsonPropertyAttribute))).PropertyName;
                Logger.D( "get ok " + jsonPropName);
                return jsonPropName;
            }
            Logger.D( $" return null");
            return null;
        }
    }
}
