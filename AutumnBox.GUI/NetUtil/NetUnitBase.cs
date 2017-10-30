/* =============================================================================*\
*
* Filename: NetUnitBase
* Description: 
*
* Version: 1.0
* Created: 2017/10/16 14:08:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Shared.CstmDebug;
using System;

namespace AutumnBox.GUI.NetUtil
{
    [LogProperty(TAG = "Net Unit")]
    public abstract class NetUnitBase : Object
    {
        protected NetUnitPropertyAttribute PropertyInfo
        {
            get
            {
                object[] objAttrs = GetType().GetCustomAttributes(typeof(NetUnitPropertyAttribute), true);
                if (objAttrs.Length > 0)
                {
                    foreach (object obj in objAttrs)
                    {
                        if (obj is NetUnitPropertyAttribute)
                        {
                            return (obj as NetUnitPropertyAttribute);
                        }
                    }
                }
                return null;
            }
        }
        protected bool UseLocalApi { get { return PropertyInfo.UseLocalApi; } }
        protected string GetHtmlCode(string url)
        {
            return Shared.NetHelper.GetHtmlCode(url);
        }
        public abstract void Run();
        //public abstract void LocalTestMethod();
        //public abstract void ApiTestMethod();
    }
}
