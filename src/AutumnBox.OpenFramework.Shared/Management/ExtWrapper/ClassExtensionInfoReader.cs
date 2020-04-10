/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 0:02:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutumnBox.Leafx.Container;

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    /// <summary>
    /// ClassExtension包装器的信息获取器
    /// </summary>
    public class ClassExtensionInfoReader : IExtensionInfoDictionary
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                try
                {
                    return Informations[key].Value;
                }
                catch (Exception e)
                {
                    SLogger.Debug(Name, $"InfoReader: can not get info '{key}' ", e);
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取的目标
        /// </summary>
        public Type ExtType { get; private set; }
        /// <summary>
        /// 已获取的特性
        /// </summary>
        public Dictionary<string, IInformationAttribute> Informations { get; private set; }
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
        public virtual byte[] Icon { get; set; } = null;
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
                return this[ExtensionInformationKeys.NAME] as string;
            }
        }
        /// <summary>
        /// 基础的说明信息
        /// </summary>
        public virtual string Desc
        {
            get
            {
                return this[ExtensionInformationKeys.DESCRIPTION] as string;
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
                        sb.AppendLine(string.Format(app.GetPublicResouce<string>("PanelExtensionsAuthFmt"), Auth));
                    }
                    if (Version != defaultVersion)
                    {
                        sb.AppendLine(string.Format(app.GetPublicResouce<string>("PanelExtensionsVersionFmt"), Version));
                    }
                    if (Desc != null)
                    {
                        sb.AppendLine(string.Format(app.GetPublicResouce<string>("PanelExtensionsDescFmt"), Desc));
                    }
                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    SLogger.Warn(nameof(ClassExtensionInfoReader), ex);
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
                return this[ExtensionInformationKeys.AUTH] as string;
            }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public IEnumerable<string> Regions { get; private set; }

        /// <summary>
        /// 获取一个扫描器
        /// </summary>
        public ClassExtensionScanner Scanner
        {
            get
            {
                if (_scanner == null)
                {
                    _scanner = new ClassExtensionScanner(ExtType);
                }
                return _scanner;
            }
        }
        private ClassExtensionScanner _scanner;

        private readonly IAppManager app;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="type"></param> 
        public ClassExtensionInfoReader(Type type)
        {
            this.ExtType = type;
            app = LakeProvider.Lake.Get<IAppManager>();
        }
        /// <summary>
        /// 重载
        /// </summary>
        public virtual void Reload()
        {
            ClassExtensionScanner scanner = this.Scanner;
            scanner.Scan();
            Informations = scanner.Informations;
            RequiredDeviceStates = (DeviceState)this[ExtensionInformationKeys.REQ_DEV_STATE];
            Version = this[ExtensionInformationKeys.VERSION] as Version;
            MinApi = (int)(this[ExtensionInformationKeys.MIN_ATMB_API] ?? BuildInfo.API_LEVEL);
            TargetApi = (int)(this[ExtensionInformationKeys.TARGET_ATMB_API] ?? BuildInfo.API_LEVEL);
            Regions = this[ExtensionInformationKeys.REGIONS] as IEnumerable<string>;
            try
            {
                Icon = ReadIcon(this[ExtensionInformationKeys.ICON].ToString());
            }
            catch (KeyNotFoundException)
            {
            }
            catch (NullReferenceException) { }
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
                return new byte[0];
            }
        }
        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGet(string key, out object result)
        {
            try
            {
                result = Informations[key].Value;
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
        /// <summary>
        /// 泛型的尝试获取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGet<TResult>(string key, out TResult result)
        {
            try
            {
                result = (TResult)Informations[key].Value;
                return true;
            }
            catch
            {
                result = default(TResult);
                return false;
            }
        }
        /// <summary>
        /// 泛型的尝试获取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetClassType<TResult>(string key, out TResult result) where TResult : class
        {
            try
            {
                result = Informations[key].Value as TResult;
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
        /// <summary>
        /// 尝试获取值类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetValueType<TResult>(string key, out TResult result) where TResult : struct
        {
            try
            {
                result = (TResult)Informations[key].Value;
                return true;
            }
            catch
            {
                result = default(TResult);
                return false;
            }
        }
    }
}
