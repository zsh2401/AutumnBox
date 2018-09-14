/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules
{
    [ExtAuth("秋之盒官方")]
    [ExtAuth("AutumnBox official", Lang = "en-us")]
    [ExtOfficial(true)]
    [ContextPermission(CtxPer.High)]
    public abstract class OfficialVisualExtension : FasterVisualExtension
    {
       
    }
}
