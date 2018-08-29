/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:22:05 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.OS
{
    public interface IDpiModifier
    {
        int GetSourceDpi();
        void SetDpi(int dpi);
    }
}
