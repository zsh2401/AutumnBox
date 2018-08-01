/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 0:36:55 (UTC +8:00)
** desc： ...
*************************************************/
//#define SHOW_METHOD_ERR
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
#if SHOW_METHOD_ERR
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "__Name() Not found", ex);
                    return _fileName;
                }
#else
                catch
                {
                    return _fileName;
                }
#endif
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
#if SHOW_METHOD_ERR
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "__Desc() Not found", ex);
                    return "";
                }
#else
                catch
                {
                    return "";
                }
#endif
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
#if SHOW_METHOD_ERR
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "__Version() Not found", ex);
                    return new Version(1, 0, 0, 0);
                }
#else
                catch
                {
                    return new Version(1, 0, 0, 0);
                }
#endif
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
#if SHOW_METHOD_ERR
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "__ContactInfo() Not found", ex);
                    return null;
                }
#else
                catch { return null; }
#endif
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
#if SHOW_METHOD_ERR
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "__Auth() Not found", ex);
                    return OpenApi.Gui.GetPublicResouce<string>(this, "lbAnonymous");
                }
#else
                catch
                {
                    return OpenApi.Gui.GetPublicResouce<string>(this, "lbAnonymous");
                }
#endif
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
#if SHOW_METHOD_ERR
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "Script stop Failed!", ex);
                return false;
            }
#else
            catch { return false; }
#endif

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
#if SHOW_METHOD_ERR
            catch (Exception ex)
            {

                OpenApi.Log.Warn(this, "__ReqState() Not found", ex);
                return true;
             }
#else
            catch { return true; }
#endif
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
#if SHOW_METHOD_ERR
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "SDestory() Not found", ex);
            }
#else
            catch { }
#endif
        }
    }
}
