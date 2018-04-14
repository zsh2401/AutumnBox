/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/14 23:37:06 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    public class ScriptApiLevel : Attribute
    {
        public int Min { get; set; }
        public int Target { get; set; }
    }
    public class ScriptContactInfo : Attribute {
        public string ContactInfo { get; private set; }
        public ScriptContactInfo(string info) {
            this.ContactInfo = info;
        }
    }
    public class ScriptAuth : Attribute
    {
        public string Auth { get; private set; }
        public ScriptAuth(string atuh)
        {
            Auth = atuh;
        }
    }
    public class ScriptVersion : Attribute
    {
        public Version Version { get; private set; }
        public ScriptVersion(int major, int minor, int build = 0, int revision = 0)
        {
            Version = new Version(major, minor, build, revision);
        }
    }
    public class ScriptName : Attribute
    {
        public string Name { get; private set; }
        public ScriptName(string name)
        {
            Name = name;
        }
    }
    public class ScriptUpdateInfo : Attribute
    {
        public int UpdateId { get; set; }
        public bool Updatable { get; set; }
    }
}
