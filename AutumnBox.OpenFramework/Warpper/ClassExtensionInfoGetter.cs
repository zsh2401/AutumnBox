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
    /// <summary>
    /// ClassExtension包装器的信息获取器
    /// </summary>
    internal class ClassExtensionInfoGetter : Context, IExtInfoGetter
    {
        private readonly Context ctx;
        public Type ExtType { get; private set; }
        private static readonly string DescFMT =
       "{0}: {1}" + Environment.NewLine +
       "{2}:" + Environment.NewLine +
       "{3}";
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
        public DeviceState RequiredDeviceStates { get; private set; }
        public string Name
        {
            get
            {
                return GetInfoByCurrentLanguage(nameof(ExtNameAttribute));
            }
        }
        public string Desc
        {
            get
            {
                return GetInfoByCurrentLanguage(nameof(ExtDescAttribute));
            }
        }
        public string FormatedDesc
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
                return GetInfoByCurrentLanguage(nameof(ExtAuthAttribute));
            }
        }
        public bool Visual { get; private set; }
        public ClassExtensionInfoGetter(Context ctx, Type type)
        {
            this.ctx = ctx;
            this.ExtType = type;
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
        public void Reload()
        {
            var extInfoAttr = ExtType.GetCustomAttributes(typeof(ExtInfoAttribute), true);
            ExtInfoAttribute current = null;
            for (int i = 0; i < extInfoAttr.Length; i++)
            {
                current = (ExtInfoAttribute)extInfoAttr[i];
                infoTable.Add(current.Key, current);
            }
            RequiredDeviceStates = (DeviceState)infoTable[nameof(ExtRequiredDeviceStatesAttribute)].Value;
            Version = infoTable[nameof(ExtVersionAttribute)].Value as Version;
            RunAsAdmin = (bool)infoTable[nameof(ExtRunAsAdminAttribute)].Value;
            MinApi = (int)infoTable[nameof(ExtMinApiAttribute)].Value;
            TargetApi = (int)infoTable[nameof(ExtTargetApiAttribute)].Value;
            Visual = (bool)infoTable[nameof(ExtUxEnableAttribute)].Value;
            try
            {
                Icon = ReadIcon(infoTable[nameof(ExtIconAttribute)].Value.ToString());
            }
            catch (KeyNotFoundException)
            {
            }
        }
        private byte[] ReadIcon(string iconPath)
        {
            try
            {
                string path = ExtType.Assembly.GetName().Name + "." + iconPath;
                Logger.Info($"getting " + path);
                Stream stream = ExtType.Assembly.GetManifestResourceStream(path);
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
