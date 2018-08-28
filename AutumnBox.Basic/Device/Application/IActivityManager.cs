/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:34:23 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Android
{
    public interface IActivityManager
    {
        void StartActivity(string pkgName,string activityPath);
    }
}
