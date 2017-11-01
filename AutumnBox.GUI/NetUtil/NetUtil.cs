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
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using System;
using System.Threading;

namespace AutumnBox.GUI.NetUtil
{
    [LogProperty(TAG = "Net Unit", Show = false)]
    internal abstract class NetUtil : Object
    {
        protected NetUtilPropertyAttribute PropertyInfo
        {
            get
            {
                object[] objAttrs = GetType().GetCustomAttributes(typeof(NetUtilPropertyAttribute), true);
                if (objAttrs.Length > 0)
                {
                    foreach (object obj in objAttrs)
                    {
                        if (obj is NetUtilPropertyAttribute)
                        {
                            return (obj as NetUtilPropertyAttribute);
                        }
                    }
                }
                return null;
            }
        }
        protected bool UseLocalApi { get { return PropertyInfo.UseLocalApi; } }
        protected string GetHtmlCode(string url)
        {
            return NetHelper.GetHtmlCode(url);
        }
        public event NetUtilFinishEventHandler Finished;
        [LogProperty(Show = false)]
        public void Run()
        {
            if (PropertyInfo.MustAddFininshedEventHandler && Finished == null)
            {
                throw new Exception("please add finish event handler");
            }
            Logger.D("Start...");
            new Thread(_Run).Start();
        }
        private void _Run()
        {
            try
            {
                NetUtilResult result;
                if (UseLocalApi)
                {
                    result = LocalMethod();
                }
                else
                {
                    result = NetMethod();
                }
                Logger.D("maybe finish...");
                Finished?.Invoke(this, new NetUtilFinishEventArgs() { Result = result });
            }
            catch (Exception e) { Logger.D("Net unit fail,", e); }
        }
        public abstract NetUtilResult LocalMethod();
        public abstract NetUtilResult NetMethod();
    }
}
