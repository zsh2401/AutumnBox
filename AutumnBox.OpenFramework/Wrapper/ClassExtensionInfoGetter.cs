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
using System.Linq;
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
        /// 获取信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return Infomations[key].Value;
            }
        }
        /// <summary>
        /// 获取的木白哦
        /// </summary>
        public Type ExtType { get; private set; }
        /// <summary>
        /// 已获取的特性
        /// </summary>
        public Dictionary<string, IInformationAttribute> Infomations { get; private set; }
        /// <summary>
        /// 最低API
        /// </summary>
        public virtual int MinApi { get; set; }
        /// <summary>
        /// 目标API
        /// </summary>
        public virtual int TargetApi { get; set; }
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
                return this[ExtNameAttribute.DEFAULT_KEY] as string;
            }
        }
        /// <summary>
        /// 基础的说明信息
        /// </summary>
        public virtual string Desc
        {
            get
            {
                return this[ExtDescAttribute.DEFAULT_KEY] as string;
            }
        }
        private readonly Version defaultVersion = new Version(0, 0, 0, 0);
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
                    if (Auth != null)
                    {
                        sb.AppendLine(string.Format(ctx.App.GetPublicResouce<string>("PanelExtensionsAuthFmt"), Auth));
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
                return this[ExtAuthAttribute.DEFAULT_KEY] as string;
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
        /// 重载
        /// </summary>
        public virtual void Reload()
        {
            ClassExtensionScanner scanner = new ClassExtensionScanner(this.ExtType);
            scanner.Scan(ClassExtensionScanner.ScanOption.Informations);
            Infomations = scanner.Informations;
            RequiredDeviceStates = (DeviceState)this[nameof(ExtRequiredDeviceStatesAttribute)];
            Version = this[nameof(ExtVersionAttribute)] as Version;
            MinApi = (int)this[nameof(ExtMinApiAttribute)];
            TargetApi = (int)this[nameof(ExtTargetApiAttribute)];
            Regions = this[nameof(ExtRegionAttribute)] as IEnumerable<string>;
            try
            {
                Icon = ReadIcon(this[nameof(ExtIconAttribute)].ToString());
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
