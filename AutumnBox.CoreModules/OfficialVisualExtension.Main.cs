/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules
{
    [ExtAuth("秋之盒官方", "en-us:AutumnBox official")]
    [ExtOfficial(true)]
    [ContextPermission(CtxPer.High)]
    internal abstract partial class OfficialVisualExtension : StrictVisualExtension
    {
        protected string Res(string key)
        {
            string result = key;
            App.RunOnUIThread(() =>
            {
                result = CoreLib.Current.Languages.Get(key) ?? PublicRes(key);
            });
            return result;
        }
    }
}
