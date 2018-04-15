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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本运行参数
    /// </summary>
    public class ScriptArgs
    {
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo DeviceInfo { get; set; }
        /// <summary>
        /// 上下文
        /// </summary>
        public Context Context { get; internal set; }
    }
    /// <summary>
    /// Script管理器
    /// </summary>
    public sealed class ABEScript : Context, IExtensionScript
    {
        static ABEScript()
        {
            CSScript.EvaluatorConfig.Engine = EvaluatorEngine.CodeDom;
        }
        /// <summary>
        /// 标签
        /// </summary>
        public override string Tag => $"{Name}_Script";
        /// <summary>
        /// 获取脚本名称
        /// </summary>
        public string Name
        {
            get
            {
                try
                {
                    return (string)_script.GetStaticMethod("__Name")();
                }
                catch (Exception ex)
                {
                    return _fileName;
                }
            }
        }
        /// <summary>
        /// 获取脚本说明
        /// </summary>
        public string Desc
        {
            get
            {
                try
                {
                    return (string)_script.GetStaticMethodWithArgs("*.__Desc")();
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 用于更新检测的ID号
        /// </summary>
        public int UpdateId
        {
            get
            {
                try
                {
                    return (int)_script.GetStaticMethodWithArgs("*.__UpdateId")();
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "Get Update id failed", ex);
                    return -1;
                }
            }
        }
        /// <summary>
        /// 获取脚本版本号
        /// </summary>
        public Version Version
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
        public string ContactInfo
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
        public string Infomation
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
        public string Auth
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
        /// 结束源
        /// </summary>
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
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
            var mainMethod = InnerScript.GetStaticMethodWithArgs("*.Main", typeof(ScriptArgs));
            _path = path;
            MainMethod = mainMethod;
            _fileName = Path.GetFileName(_path);
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="finishedCallback">执行完毕后的回调函数</param>
        public void RunAsync(ExtensionStartArgs args, Action<bool> finishedCallback = null)
        {
            Task.Run(() => {
                Run(args);
                finishedCallback?.Invoke(lastResult);
            });
        }
        private bool lastResult = true;
        /// <summary>
        /// 同步运行
        /// </summary>
        /// <param name="args"></param>
        public void Run(ExtensionStartArgs args)
        {
            var task = new Task(() =>
            {
                try
                {
                    ScriptArgs sargs = new ScriptArgs()
                    {
                        Context = this,
                        DeviceInfo = args.DeviceInfo
                    };
                    MainMethod(sargs);
                    lastResult = true;
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "发生严重错误", ex);
                    var wasFailedMsg = $"{Name} {OpenApi.Gui.GetPublicResouce<String>(this, "msgExtensionWasFailed")}";
                    OpenApi.Gui.ShowMessageBox(this, Name, wasFailedMsg);
                    lastResult = false;
                }
            }, cancellationTokenSource.Token);
            task.RunSynchronously();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public bool Stop(ExtensionStopArgs args)
        {
            try
            {
                cancellationTokenSource.Cancel();
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "Script Cancel Failed!", ex);
            }
            return true;
        }
        /// <summary>
        /// 运行前检测
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool RunCheck(ExtensionRunCheckArgs args)
        {
            return true;
        }
    }
}
