/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 21:14:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using System;

namespace AutumnBox.OpenFramework.Open.Impl
{
    /// <summary>
    /// 日志器实现
    /// </summary>
    internal class LoggerImpl : ILogger
    {
        private Context ctx;
        public LoggerImpl(Context ctx)
        {
            this.ctx = ctx;
        }

        public string Tag => ctx.LoggingTag;

        public void Debug(string msg)
        {
            CallingBus.BaseApi.Log(ctx.LoggingTag, nameof(Debug), msg);
        }

        public void Fatal(string msg)
        {
            CallingBus.BaseApi.Log(ctx.LoggingTag, nameof(Fatal), msg);
        }

        public void Info(string msg)
        {
            CallingBus.BaseApi.Log(ctx.LoggingTag, nameof(Info), msg);
        }

        public void Warn(string msg)
        {
            CallingBus.BaseApi.Log(ctx.LoggingTag, nameof(Warn), msg);
        }

        public void Warn(string msg, Exception ex)
        {
            CallingBus.BaseApi.Log(ctx.LoggingTag, nameof(Warn), msg + Environment.NewLine + ex);
        }
    }
}
