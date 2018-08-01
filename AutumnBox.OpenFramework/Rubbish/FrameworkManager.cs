/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/21 14:44:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Script;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Internal
{
    /// <summary>
    /// 请勿访问此处
    /// </summary>
    public class FrameworkManager
    {
        /// <summary>
        /// 拓展路径
        /// </summary>
        public string ExtensionsPath => ExtensionManager.ExtensionsPath_Internal;
        private readonly Context srcContext;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="ctx"></param>
        public FrameworkManager(Context ctx)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Mid);
            this.srcContext = ctx;
        }
        /// <summary>
        /// 初始化调试API(权限要求:中)
        /// </summary>
        /// <param name="ctx">设置者</param>
        /// <param name="apiImpl">API实现</param>
        public void SetLogApi(ILogApi apiImpl)
        {
            OpenApi.Log = apiImpl;
        }
        /// <summary>
        /// 初始化图形API(权限要求:中)
        /// </summary>
        /// <param name="ctx">设置者</param>
        /// <param name="apiImpl">API实现</param>
        public void SetGuiApi(IAppGuiManager apiImpl)
        {
            OpenApi.Gui = apiImpl;
        }
        /// <summary>
        /// 加载所有拓展
        /// </summary>
        public void LoadAllExtension()
        {
            ExtensionManager.LoadAllExtension(srcContext);
        }
        /// <summary>
        /// 摧毁所有拓展
        /// </summary>
        public void DestoryAllExtension()
        {
            ExtensionManager.DestoryAllExtension(srcContext);
        }
        /// <summary>
        /// 重载所有脚本
        /// </summary>
        public void ReloadAllScript()
        {
            ScriptsManager.ReloadAll(srcContext);
        }
        /// <summary>
        /// 卸载所有脚本
        /// </summary>
        public void UnloadAllScript()
        {
            ScriptsManager.UnloadAll(srcContext);
        }
        /// <summary>
        /// 获取所有标准拓展
        /// </summary>
        /// <returns></returns>
        public IAutumnBoxExtension[] GetStdExtensions()
        {
            return ExtensionManager.GetExtensions(srcContext);
        }
        /// <summary>
        /// 获取所有拓展脚本
        /// </summary>
        /// <returns></returns>
        public IExtensionScript[] GetScriptExtensions()
        {
            return ScriptsManager.GetScripts(srcContext);
        }
        /// <summary>
        /// 获取所有拓展
        /// </summary>
        /// <returns></returns>
        public IExtension[] GetExtensions()
        {
            var result = new List<IExtension>();
            result.AddRange(GetStdExtensions());
            result.AddRange(GetScriptExtensions());
            return result.ToArray();
        }
    }
}
