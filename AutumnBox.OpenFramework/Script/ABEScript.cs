/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 0:36:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open;
using CSScriptLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// Script管理器
    /// </summary>
    public sealed class ABEScript : ScriptBase, IExtensionScript
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
                catch
                {
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
                catch
                {
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
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public override string FilePath
        {
            get
            {
                return _path;
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
                catch
                {
                    return OpenApi.Gui.GetPublicResouce<string>(this,"lbAnonymous");
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
            _fileName = System.IO.Path.GetFileName(_path);
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
