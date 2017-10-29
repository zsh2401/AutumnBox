/* =============================================================================*\
*
* Filename: JsonPropTest
* Description: 
*
* Version: 1.0
* Created: 2017/10/29 16:45:29 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.ObjTest
{
    [JsonObject(MemberSerialization.OptOut)]
    public class JsonPropTest
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = "haha";
        [JsonProperty("NamesJsonProp")]
        public string[] Names { get; set; } = { "x", "x" };
        public void Fuck()
        {
            //Console.WriteLine(nameof(Names));
        }
        public static void Run()
        {
            var fuck = new JsonPropTest();
            Console.WriteLine(DataHelper.JsonPropertyNameOf(fuck, nameof(fuck.Names)));
            //string json = JsonConvert.SerializeObject(new JsonPropTest());
            //var props = new JsonPropTest().GetType().GetProperties();

            //foreach (var prop in props)
            //{
            //    Console.WriteLine(prop.Name);
            //    if (!prop.IsDefined(typeof(JsonPropertyAttribute), true)) continue;
            //    //var pAttrs = prop.GetCustomAttributes(false);
            //    var j = (JsonPropertyAttribute)Attribute.GetCustomAttribute(prop, typeof(JsonPropertyAttribute));
            //    Console.WriteLine(j.PropertyName);
            //}
        }
    }
}
