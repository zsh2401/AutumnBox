/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 0:02:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AutumnBox.OpenFramework.Warpper
{
    internal class ExtensionInfoGetter
    {
        private readonly Context ctx;
        private readonly Type type;
        private static readonly string DescFMT =
       "{0}: {1}" + Environment.NewLine +
       "{2}:" + Environment.NewLine +
       "{3}:";
        public Dictionary<string, ExtInfoAttribute> Attributes
        {
            get
            {
                return infoTable;
            }
        }
        private readonly Dictionary<string, ExtInfoAttribute> infoTable
            = new Dictionary<string, ExtInfoAttribute>();
        public int MinApi { get; private set; }
        public int TargetApi { get; private set; }
        public bool RunAsAdmin { get; private set; }
        public byte[] Icon { get; private set; }
        public Version Version { get; private set; }
        public DeviceState RequiredStates { get; private set; }

        public string Name
        {
            get
            {
                return GetInfoByCurrentLanguage("ExtName");
            }
        }
        public string Desc
        {
            get
            {
                return GetInfoByCurrentLanguage("ExtDesc");
            }
        }
        public string FullDesc
        {
            get
            {

                return string.Format(DescFMT,
                    ctx.App.GetPublicResouce("lbAuth"), Auth,
                    ctx.App.GetPublicResouce("lbDescription"), Desc);
            }
        }
        public string Auth
        {
            get
            {
                return GetInfoByCurrentLanguage("ExtAuth");
            }
        }
        public ExtensionInfoGetter(Context ctx, Type type)
        {
            this.ctx = ctx;
            this.type = type;
        }
        private string GetInfoByCurrentLanguage(string key)
        {
            string lanCode = ctx.App.CurrentLanguageCode.ToLower();
            try
            {
                var keyWithLang = key + "_" + lanCode;
                return (string)(infoTable[keyWithLang].Value);
            }
            catch { }
            try
            {
                return (string)(infoTable[key].Value);
            }
            catch { }
            throw new KeyNotFoundException("cannot found target or default language");
        }
        public void Load()
        {
            var extInfoAttr = type.GetCustomAttributes(typeof(ExtInfoAttribute), true);
            ExtInfoAttribute current = null;
            for (int i = 0; i < extInfoAttr.Length; i++)
            {
                current = (ExtInfoAttribute)extInfoAttr[i];
                infoTable.Add(current.Key, current);
            }
            RequiredStates = (DeviceState)infoTable["ExtRequiredDeviceStates"].Value;
            Icon = ReadIcon(infoTable["ExtIcon"].Value.ToString());
            Version = infoTable["ExtVersion"].Value as Version;
            RunAsAdmin = (bool)infoTable["ExtRunAsAdmin"].Value;
            MinApi = (int)infoTable["ExtMinApi"].Value;
            TargetApi = (int)infoTable["ExtTargetApi"].Value;
        }
        private byte[] ReadIcon(string iconPath)
        {
            try
            {
                string path = type.Namespace + "." + iconPath;
                Stream stream = type.Assembly.GetManifestResourceStream(path);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new byte[0];
            }
        }
    }
}
