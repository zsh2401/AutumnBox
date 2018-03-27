/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 17:52:48 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Support.Log;
using System;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 高级输出,相比父类多了个返回码
    /// </summary>
    public class AdvanceOutput : Output
    {
        private int exitCode;
        /// <summary>
        /// 获取返回码
        /// </summary>
        /// <returns></returns>
        public int GetExitCode()
        {
            return exitCode;
        }
        /// <summary>
        /// 根据返回码判断是否成功
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return GetExitCode() == 0;
            }
        }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="source"></param>
        /// <param name="exitCode"></param>
        public AdvanceOutput(Output source, int exitCode) : base(source.All, source.Out, source.Error)
        {
            this.exitCode = exitCode;
        }

        /// <summary>
        /// 自定义tag,并打印AdvanceOutput在Log
        /// </summary>
        /// <param name="tagOrSender"></param>
        /// <param name="printOnRelease">log级别</param>
        public override void PrintOnLog(object tagOrSender, bool printOnRelease=false)
        {
            if (printOnRelease)
            {
                Logger.Info(tagOrSender, $"{this.GetType().Name}.PrintOnLog():{Environment.NewLine}" +
                    $"Exit Code: {exitCode}{Environment.NewLine}" +
                    $"{ToString()}");
            }
            else
            {
                Logger.Debug(tagOrSender, $"{this.GetType().Name}.PrintOnLog():{Environment.NewLine}" +
                    $"Exit Code: {exitCode}{Environment.NewLine}" +
                    $"{ToString()}");
            }
        }
        /// <summary>
        /// 自定义tag,并打印AdvanceOutput在控制台
        /// </summary>
        /// <param name="tagOrSender"></param>
        public override void PrintOnConsole(object tagOrSender)
        {
            Console.WriteLine($"{tagOrSender}.{this.GetType().Name}.PrintOnLog():{Environment.NewLine}" +
                $"Exit Code: {exitCode}" +
                $"{ToString()}");
        }
    }
}
