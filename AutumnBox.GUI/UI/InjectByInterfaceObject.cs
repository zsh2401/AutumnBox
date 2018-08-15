/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 20:25:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.UI
{
    class InjectByInterfaceObject : Object
    {
        public static class Manager
        {
            public static void AutoAnalyze(object obj)
            {
                throw new NotImplementedException();
            }
            public static List<IDependOnDeviceChanges> deviceChangeDependers = new List<IDependOnDeviceChanges>();
        }
        public InjectByInterfaceObject()
        {
            if (this is IDependOnDeviceChanges dep)
            {
                Manager.deviceChangeDependers.Add(dep);
            }
        }
    }
}
