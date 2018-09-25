/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/22 23:58:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management
{
    public interface INetFxApiCaller
    {
        AppDomain GetExtAppDomain();
        AutumnBoxExtension GetInstanceFrom(AppDomain appDomain,Type extType);
    }
}
