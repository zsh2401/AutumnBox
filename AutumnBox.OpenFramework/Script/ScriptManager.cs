/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/14 23:20:20 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本管理器
    /// </summary>
    public class ScriptManager:IAutumnBoxExtension
    {
        /// <summary>
        /// 脚本文件的路径
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// 获取具体的脚本
        /// </summary>
        public IExtensionScript Script { get { return abeScript; } }
        /// <summary>
        /// 脚本的实现
        /// </summary>
        private ABEScript abeScript;
        /// <summary>
        /// 源代码
        /// </summary>
        public string SourceCode { get; private set; }

        public int? TargetSdk => throw new NotImplementedException();

        public int? MinSdk => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Infomation => throw new NotImplementedException();

        public object Icon => throw new NotImplementedException();

        /// <summary>
        /// 更新脚本
        /// </summary>
        public void Update() { throw new NotImplementedException(); }
        /// <summary>
        /// 检测更新
        /// </summary>
        /// <returns></returns>
        public bool CheckUpdate() { throw new NotImplementedException(); }
        /// <summary>
        /// 重载脚本
        /// </summary>
        public void Reload() { throw new NotImplementedException(); }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fileName"></param>
        private ScriptManager(string fileName) { throw new NotImplementedException(); }
        /// <summary>
        /// 从文件加载脚本
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ScriptManager LoadFromFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool RunCheck(RunCheckArgs args)
        {
            return true;
        }

        public bool Init(InitArgs args)
        {
            return true;
        }

        public void Run(StartArgs args)
        {
            abeScript.Run(args);
        }

        public bool Stop(StopArgs args)
        {
            return false;
        }

        public void Destory(DestoryArgs args)
        {
        }

        public void Clean(CleanArgs args)
        {
        }

        public void Dispose()
        {
        }
    }
}
