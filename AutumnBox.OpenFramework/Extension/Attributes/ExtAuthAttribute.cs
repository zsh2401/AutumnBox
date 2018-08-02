/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:06:51 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.OpenFramework.Extension.Attributes
{
    public class ExtAuthAttribute : ExtAttribute
    {
        public readonly string Auth;
        public ExtAuthAttribute(string auth)
        {
            this.Auth = auth;
        }
    }
}
