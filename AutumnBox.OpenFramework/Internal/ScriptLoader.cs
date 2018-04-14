/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/14 22:49:34 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Script;
using CSScriptLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{
    public static class ScriptLoader
    {
        public static void Test() {
            CSScript.EvaluatorConfig.Engine = EvaluatorEngine.CodeDom;
            var main = CSScript.LoadMethod(File.ReadAllText("Test.cs")).GetStaticMethod("*.GetScriptInfo");
            ScriptInfo info =  (ScriptInfo)main();
            Console.WriteLine(info.Name);
        }
    }
}
