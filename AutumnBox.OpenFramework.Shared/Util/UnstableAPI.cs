using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.LeafExtension.View;
using AutumnBox.OpenFramework.Management;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Util
{
    /// <summary>
    /// 通用性较差的API,非常不建议调用
    /// </summary>
    public static class UnstableAPI
    {
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
                dialogTask = ui.ShowLeafDialog((ILeafDialog)GetViewById(viewId));
#endif
            });
            return dialogTask;
        }
    }
}
