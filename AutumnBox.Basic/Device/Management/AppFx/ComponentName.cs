using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    public class ComponentName
    {
        public string PackageName { get; private set; }
        public string ClassName { get; private set; }
        public ComponentName(string pkgName,string className) {
            this.PackageName = pkgName;
            this.ClassName = className;
        }
        public override string ToString()
        {
            return $"{PackageName}/.{ClassName}";
        }
    }
}
