/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 17:52:48 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;

namespace AutumnBox.Basic.Data
{
    /// <summary>
    /// 高级输出,相比父类多了个返回码
    /// </summary>
    public class AdvanceOutput : Output, IEquatable<AdvanceOutput>
    {
        /// <summary>
        /// 返回码
        /// </summary>
        private int exitCode;

        /// <summary>
        /// 空输出，此字段为只读
        /// </summary>
        public static new readonly AdvanceOutput Empty = new AdvanceOutput(Output.Empty, 0);

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
        public override void PrintOnLog(object tagOrSender, bool printOnRelease = false)
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

        /// <summary>
        /// 与other比较是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as AdvanceOutput);
        }

        /// <summary>
        /// 与other比较是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AdvanceOutput other)
        {
            return other != null &&
                   base.Equals(other) &&
                   exitCode == other.exitCode;
        }

        /// <summary>
        /// 哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -1910581217;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + exitCode.GetHashCode();
            hashCode = hashCode * -1521134295 + IsSuccessful.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// 字符化
        /// </summary>
        /// <returns></returns>
        public string ToStringWithExitCode()
        {
            return $"{exitCode}{Environment.NewLine}{base.ToString()}";
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="output1"></param>
        /// <param name="output2"></param>
        /// <returns></returns>
        public static bool operator ==(AdvanceOutput output1, AdvanceOutput output2)
        {
            return EqualityComparer<AdvanceOutput>.Default.Equals(output1, output2);
        }

        /// <summary>
        /// 不等
        /// </summary>
        /// <param name="output1"></param>
        /// <param name="output2"></param>
        /// <returns></returns>
        public static bool operator !=(AdvanceOutput output1, AdvanceOutput output2)
        {
            return !(output1 == output2);
        }
    }
}
