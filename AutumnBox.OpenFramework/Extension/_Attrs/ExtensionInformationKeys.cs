/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/21 0:28:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块信息键标准
    /// </summary>
    public static class ExtensionInformationKeys
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public const string PRIORITY = "Priority";
        public const string ICON = "Icon";
        public const string AUTH = "Auth";
        public const string NAME = "Name";
        public const string VERSION = "VERSION";
        public const string DESCRIPTION = "Desc";
        public const string REQ_DEV_STATE = "ReqDevState";
        public const string REGIONS = "Regions";
        public const string MIN_ATMB_API = "MinAtmbApi";
        public const string TARGET_ATMB_API = "TgtAtmbApi";
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
