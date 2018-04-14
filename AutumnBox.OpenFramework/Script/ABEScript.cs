/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 0:36:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal;
using CSScriptLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    public class ScriptArgs
    {
        public DeviceBasicInfo DeviceInfo { get; set; }
    }
    public sealed class ABEScript
    {
        static ABEScript()
        {
            CSScript.EvaluatorConfig.Engine = EvaluatorEngine.CodeDom;
        }
        private string _path;
        private string _fileName;
        private MethodDelegate MainMethod;
        private MethodDelegate GetScriptInfoMethod;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="fileName"></param>
        public ABEScript(string fileName)
        {
            var extPath = ExtensionManager.ExtensionsPath;
            var path = Path.Combine(extPath, fileName);
            Reload(path);
        }
        /// <summary>
        /// 从文件加载
        /// </summary>
        /// <param name="path"></param>
        public void Reload(string path)
        {
            var src = File.ReadAllText(path);
            var script = CSScript.LoadMethod(src);
            var mainMethod = script.GetStaticMethod("*.Main", typeof(ScriptArgs));
            try
            {
                var getScriptInfoMethod = script.GetStaticMethod("*.GetScriptInfo");
                GetScriptInfoMethod = getScriptInfoMethod;
            }
            catch
            {
                var getScriptInfoMethod = script.GetStaticMethod("*.GetScriptInfo");
            }
            _path = path;
            MainMethod = mainMethod;
            _fileName = Path.GetFileName(_path);
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        public void Run(ScriptArgs args)
        {
            MainMethod(args);
        }
        /// <summary>
        /// 获取脚本信息
        /// </summary>
        public ScriptInfo Info
        {
            get
            {
                try
                {
                    return (ScriptInfo)GetScriptInfoMethod();
                }
                catch
                {
                    return new ScriptInfo() { Name = _fileName };
                }
            }
        }
    }
}
