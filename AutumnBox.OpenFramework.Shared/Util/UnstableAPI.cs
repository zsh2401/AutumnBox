using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Util
{
    /// <summary>
    /// 通用性较差的API,非常不建议调用
    /// </summary>
    public static class UnstableAPI
    {
        /// <summary>
        /// 在LeafUI显示对话
        /// </summary>
        /// <param name="ui">LeafUI</param>
        /// <param name="content">内容,推荐为一个View</param>
        /// <returns>对话任务</returns>
        public static Task<object> ShowDialog(this ILeafUI ui, object content)
        {
            Task<object> dialogTask = null;
            ui.RunOnUIThread(() =>
            {
#if!SDK
                dialogTask = ui._ShowDialog(content);
#endif
            });
            return dialogTask;
        }
        /// <summary>
        /// 请在UI线程操作,获取LeafUI DialogHost(MaterialDesignInXaml)
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public static object GetDialogHost(this ILeafUI ui)
        {
#if !SDK
            return ui._GetDialogHost();
#else
            throw new Exception();
#endif
        }
        /// <summary>
        /// 请在UI线程操作,根据ID获取秋之盒View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetViewById(string id)
        {
            return OpenFx.BaseApi.GetNewView(id);
        }
        /// <summary>
        /// 显示对话
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public static Task<object> ShowDialogById(this ILeafUI ui, string viewId)
        {
            Task<object> dialogTask = null;
            ui.RunOnUIThread(() =>
            {
#if !SDK
                dialogTask = ui.ShowDialog(GetViewById(viewId));
#endif
            });
            return dialogTask;
        }
    }
}
