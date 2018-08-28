/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:31:40 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.OS
{
    /// <summary>
    /// BuildProp获取器
    /// </summary>
    public class BuildPropGetter : DependOnDeviceObject
    {
        public BuildPropGetter(IDevice device) : base(device)
        {
        }

        public string this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public void Reload()
        {
            throw new NotImplementedException();
        }
    }
}
