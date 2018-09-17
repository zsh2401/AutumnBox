/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 0:02:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutumnBox.OpenFramework.Wrapper
{
    /// <summary>
    /// ClassExtension包装器的信息获取器
    /// </summary>
    public class ClassExtensionInfoGetter : Context, IExtInfoGetter
    {
        private readonly Context ctx;
        /// <summary>
        /// 获取的木白哦
        /// </summary>
        public Type ExtType { get; private set; }
        /// <summary>
        /// 已获取的特性
        /// </summary>
        public Dictionary<string, ExtInfoAttribute> Attributes
        {
            get
            {
                return infoTable;
            }
        }
        /// <summary>
        /// 表
        /// </summary>
        private readonly Dictionary<string, ExtInfoAttribute> infoTable
            = new Dictionary<string, ExtInfoAttribute>();
        /// <summary>
        /// 最低API
        /// </summary>
        public virtual int MinApi { get; set; }
        /// <summary>
        /// 目标API
        /// </summary>
        public virtual int TargetApi { get; set; }
        /// <summary>
        /// 是否需要以系统级别管理员权限运行
        /// </summary>
        public virtual bool RunAsAdmin { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public virtual byte[] Icon { get; set; }
        /// <summary>
        /// 拓展模块版本号
        /// </summary>
        public virtual Version Version { get; set; }
        /// <summary>
        /// 运行所需设备状态
        /// </summary>
        public virtual DeviceState RequiredDeviceStates { get; set; }
        /// <summary>
        /// 拓展模块名
        /// </summary>
        public virtual string Name
        {
            get
            {
                return GetInfoByCurrentLanguage(nameof(ExtNameAttribute));
            }
        }
        /// <summary>
        /// 基础的说明信息
        /// </summary>
        public virtual string Desc
        {
            get
            {
                return GetInfoByCurrentLanguage(nameof(ExtDescAttribute));
            }
        }
        private readonly Version defaultVersion = new Version(0,0,0,0);
        /// <summary>
        /// 进行了挂载的说明信息
        /// </summary>
        public virtual string FormatedDesc
        {
            get
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    if (Auth != null) {
                        sb.AppendLine(string.Format(ctx.App.GetPublicResouce<string>("PanelExtensionsAuthFmt"),Auth));
                    }
                    if (Version != defaultVersion)
                    {
                        sb.AppendLine(string.Format(ctx.App.GetPublicResouce<string>("PanelExtensionsVersionFmt"), Version));
                    }
                    if (Desc != null)
                    {
                        sb.AppendLine(string.Format(ctx.App.GetPublicResouce<string>("PanelExtensionsDescFmt"), Desc));
                    }
                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    Logger.Warn("", ex);
                    return "ERROR";
                }
            }
        }
        /// <summary>
        /// 所有者
        /// </summary>
        public virtual string Auth
        {
            get
            {
                return GetInfoByCurrentLanguage(nameof(ExtAuthAttribute));
            }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public IEnumerable<string> Regions { get; private set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="type"></param>
        public ClassExtensionInfoGetter(Context ctx, Type type)
        {
            this.ctx = ctx;
            this.ExtType = type;
        }
        /// <summary>
        /// 根据当前语言获取I18N特性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string GetInfoByCurrentLanguage(string key)
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
        /// <summary>
        /// 重载
        /// </summary>
        public virtual void Reload()
        {
            var extInfoAttr = ExtType.GetCustomAttributes(typeof(ExtInfoAttribute), true);
            ExtInfoAttribute current = null;
            //Logger.CDebug("ExtName:" + ExtType.Name);
            for (int i = 0; i < extInfoAttr.Length; i++)
            {
                current = (ExtInfoAttribute)extInfoAttr[i];
                //Logger.CDebug("ExtAttrKey"  + current);
                infoTable.Add(current.Key, current);
            }
            RequiredDeviceStates = (DeviceState)infoTable[nameof(ExtRequiredDeviceStatesAttribute)].Value;
            Version = infoTable[nameof(ExtVersionAttribute)].Value as Version;
            RunAsAdmin = (bool)infoTable[nameof(ExtRunAsAdminAttribute)].Value;
            MinApi = (int)infoTable[nameof(ExtMinApiAttribute)].Value;
            TargetApi = (int)infoTable[nameof(ExtTargetApiAttribute)].Value;
            Regions = infoTable[nameof(ExtRegionAttribute)].Value as IEnumerable<string>;
            try
            {
                Icon = ReadIcon(infoTable[nameof(ExtIconAttribute)].Value.ToString());
            }
            catch (KeyNotFoundException)
            {
            }
        }
        /// <summary>
        /// 加载图标
        /// </summary>
        /// <param name="iconPath"></param>
        /// <returns></returns>
        protected virtual byte[] ReadIcon(string iconPath)
        {
            try
            {
                string path = ExtType.Assembly.GetName().Name + "." + iconPath;
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
