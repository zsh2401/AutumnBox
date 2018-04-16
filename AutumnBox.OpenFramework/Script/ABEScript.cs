/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 0:36:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Open;
using CSScriptLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// Script管理器
    /// </summary>
    public sealed class ABEScript : Script, IExtensionScript
    {
        /// <summary>
        /// 静态初始化
        /// </summary>
        static ABEScript()
        {
            Debug.WriteLine("Setting CSScript engine");
            CSScript.CleanupDynamicSources();
            CSScript.EvaluatorConfig.Engine = EvaluatorEngine.CodeDom;
            Debug.WriteLine("Setted CSScript engine");
        }
        /// <summary>
        /// 标签
        /// </summary>
        public override string Tag => $"{Name}_Script";
        /// <summary>
        /// 获取脚本名称
        /// </summary>
        public override string Name
        {
            get
            {
                try
                {
                    return (string)_script.GetStaticMethodWithArgs("*.__Name")();
                }
                catch
                {
                    return _fileName;
                }
            }
        }
        /// <summary>
        /// 获取脚本说明
        /// </summary>
        public override string Desc
        {
            get
            {
                try
                {
                    return (string)_script.GetStaticMethodWithArgs("*.__Desc")();
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "Get desc failed", ex);
                    return "";
                }
            }
        }
        /// <summary>
        /// 获取脚本版本号
        /// </summary>
        public override Version Version
        {
            get
            {
                try
                {
                    return (Version)_script.GetStaticMethodWithArgs("*.__Version")();
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "Get version failed", ex);
                    return new Version(1, 0, 0, 0);
                }
            }
        }
        /// <summary>
        /// 获取脚本联系信息
        /// </summary>
        public override string ContactInfo
        {
            get
            {
                try
                {
                    return (string)_script.GetStaticMethodWithArgs("*.__ContactInfo")();
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "Get contact info failed", ex);
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取格式化的信息
        /// </summary>
        public override string Infomation
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbVersion")}:\t{Version}");
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbAuth")}:\t{Auth}");
                    if (ContactInfo != null)
                    {
                        sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbContactEmail")}:\t{ContactInfo}");
                    }
                    sb.AppendLine(); sb.AppendLine();
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbDescription")}:");
                    sb.AppendLine($"{Desc}");
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "exception on building infomation text..", ex);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 获取脚本所有者
        /// </summary>
        public override string Auth
        {
            get
            {
                try
                {
                    return (string)_script.GetStaticMethodWithArgs("*.__Auth")();
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "Get auth failed", ex);
                    return "佚名";
                }
            }
        }
        private string _path;
        private string _fileName;
        /// <summary>
        /// 主函数
        /// </summary>
        private MethodDelegate MainMethod;
        /// <summary>
        /// 获取内部脚本程序集
        /// </summary>
        public Assembly InnerScript => _script;

        /// <summary>
        /// 内部脚本程序集
        /// </summary>
        private Assembly _script;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="context">上下文,要求中等权限</param>
        /// <param name="path">脚本完整路径</param>
        public ABEScript(Context context, string path)
        {
#if !DEBUG
            context.PermissionCheck();
#endif
            var src = File.ReadAllText(path);
            _script = CSScript.LoadMethod(src);
            var mainMethod = _script.GetStaticMethodWithArgs("*.Main", typeof(ScriptArgs));
            try
            {
                var initMethod = InnerScript.GetStaticMethodWithArgs("*.InitAndCheck", typeof(ScriptInitArgs));
                if ((bool)initMethod(new ScriptInitArgs(this)) == false)
                {
                    throw new Exception("Script cannot init");
                }
            }
            catch { }
            _path = path;
            MainMethod = mainMethod;
            _fileName = Path.GetFileName(_path);
        }
        /// <summary>
        /// 同步运行
        /// </summary>
        /// <param name="args"></param>
        public override bool Run(ExtensionStartArgs args)
        {
            try
            {
                ScriptArgs sargs = new ScriptArgs(this, args.DeviceInfo);
                MainMethod(sargs);
                return true;
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "发生严重错误", ex);
                var wasFailedMsg = $"{Name} {OpenApi.Gui.GetPublicResouce<String>(this, "msgExtensionWasFailed")}";
                OpenApi.Gui.ShowMessageBox(this, Name, wasFailedMsg);
                return false;
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public override bool Stop(ExtensionStopArgs args)
        {
            try
            {
                return (bool)_script.GetStaticMethodWithArgs("*.OnStop", typeof(ScriptStopArgs))(new ScriptStopArgs(this));
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "Script stop Failed!", ex);
                return false;
            }
        }
        /// <summary>
        /// 运行前检测
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool RunCheck(ExtensionRunCheckArgs args)
        {
            try
            {
                return ((DeviceState)_script.GetStaticMethodWithArgs("*.__ReqState")())
                    .HasFlag(args.DeviceInfo.State);
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "Get required device state Failed!", ex);
                return true;
            }
        }
        /// <summary>
        /// 析构
        /// </summary>
        public override void Dispose()
        {
            try
            {
                _script.GetStaticMethodWithArgs("*.SDestory", typeof(ScriptDestoryArgs))(new ScriptDestoryArgs(this));
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "Script dispose failed", ex);
            }

        }
    }
}
