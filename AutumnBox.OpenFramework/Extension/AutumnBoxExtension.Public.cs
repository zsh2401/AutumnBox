/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/21 23:33:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    public partial class AutumnBoxExtension
    {
        public object Icon => null;

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
                    sb.AppendLine();
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

        public bool Init(InitArgs args)
        {
            try
            {
                return InitAndCheck(args);
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "初始化失败", ex);
                return false;
            }
        }

        public bool RunCheck(RunCheckArgs args)
        {
            return RequiredDeviceState.HasFlag(args.DeviceInfo.State);
        }

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

        public void Dispose()
        {
            Destory(new DestoryArgs());
        }
    }
}
