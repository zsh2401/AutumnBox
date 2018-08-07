/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 21:14:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.Support.Log;
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
        public void Debug(string msg)
        {
            Logger.Debug(ctx.LoggingTag, msg);
        }

        public void Fatal(string msg)
        {
            Logger.Fatal(ctx.LoggingTag, msg);
        }

        public void Info(string msg)
        {
            Logger.Info(ctx.LoggingTag, msg);
        }

        public void Warn(string msg)
        {
            Logger.Warn(ctx.LoggingTag, msg);
        }

        public void Warn(string msg, Exception ex)
        {
            Logger.Warn(ctx.LoggingTag, msg, ex);
        }
    }
}
