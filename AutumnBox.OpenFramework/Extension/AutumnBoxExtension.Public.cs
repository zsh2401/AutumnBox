/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/21 23:33:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using System;
using System.Text;
namespace AutumnBox.OpenFramework.Extension
{
    public partial class AutumnBoxExtension : Context, IAutumnBoxExtension
    {
        /// <summary>
        /// 图标,暂未实现
        /// </summary>
        public object Icon => null;
        /// <summary>
        /// 信息
        /// </summary>
        public string Infomation
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbVersion")}:\t{Version}");
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbAuth")}:\t{Auth}");
                    if (ContactMail != null)
                    {
                        sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbContactEmail")}:\t{ContactMail}");
                    }
                    var lbUnspecified = OpenApi.Gui.GetPublicResouce<string>(this,"lbUnspecified");
                    sb.Append($"{OpenApi.Gui.GetPublicResouce(this, "lbMinSdk")}:\t{MinSdk?.ToString() ?? lbUnspecified}");
                    sb.Append($"\t{OpenApi.Gui.GetPublicResouce(this, "lbTargetSdk")}:\t{TargetSdk?.ToString() ?? lbUnspecified}");
                    sb.AppendLine(); sb.AppendLine();
                    sb.AppendLine($"{OpenApi.Gui.GetPublicResouce(this, "lbDescription")}:");
                    sb.AppendLine($"{Description}");
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, "exception on building infomation text..", ex);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool Init(InitArgs args)
        {
            try
            {
                if (BuildInfo.SDK_VERSION < MinSdk)
                {
                    return false;
                }
                return InitAndCheck(args);
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "初始化失败", ex);
                return false;
            }
        }
        /// <summary>
        /// 运行检测
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool RunCheck(RunCheckArgs args)
        {
            return RequiredDeviceState.HasFlag(args.DeviceInfo.State);
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        public void Run(StartArgs args)
        {
            try
            {
                OnStartCommand(args);
                OnFinished();
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "发生严重错误", ex);
                var wasFailedMsg = $"{Name} {OpenApi.Gui.GetPublicResouce<String>(this, "msgExtensionWasFailed")}";
                OpenApi.Gui.ShowMessageBox(this, Name, wasFailedMsg);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool Stop(StopArgs args)
        {
            try
            {
                return OnStopCommand(args);
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "停止时发生异常", ex);
                return false;
            }
        }
        /// <summary>
        /// 摧毁
        /// </summary>
        /// <param name="args"></param>
        public void Destory(DestoryArgs args)
        {
            try
            {
                OnDestory(args);
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "摧毁时发生异常", ex);
            }
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            Destory(new DestoryArgs());
        }
    }
}
