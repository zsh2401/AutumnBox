/* =============================================================================*\
*
* Filename: OutputData.cs
* Description: 
*
* Version: 1.0
* Created: 9/15/2017 16:01:48(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Executer
{
    using AutumnBox.Basic.Util;
    using AutumnBox.Support.Log;
    using System;
    /// <summary>
    /// 输出封装类
    /// </summary>
    public class Output : IPrintable
    {
        /// <summary>
        /// 所有的输出
        /// </summary>
        public string[] LineAll { get; private set; }

        /// <summary>
        /// 所有的标准输出
        /// </summary>
        public string[] LineOut { get; private set; }

        /// <summary>
        /// 所有的标准错误
        /// </summary>
        public string[] LineError { get; private set; }

        /// <summary>
        /// 所有的输出
        /// </summary>
        public string All { get; protected set; }

        /// <summary>
        /// 所有的标准输出
        /// </summary>
        public string Out { get; protected set; }
        /// <summary>
        /// 所有的标准错误
        /// </summary>
        public string Error { get; protected set; }

        /// <summary>
        /// 判断输出中是否包含某段字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public bool Contains(string str, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                return All.ToLower().Contains(str.ToLower());
            }
            else
            {
                return All.Contains(str);
            }
        }

        /// <summary>
        /// 获取完整的输出数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return All.ToString();
        }

        /// <summary>
        /// 构建一个空的Output对象
        /// </summary>
        public Output()
        {
            this.Out = "";
            this.Error = "";
            this.All = "";
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="all">所有内容</param>
        /// <param name="stdOutput">标准输出</param>
        /// <param name="stdError">标准错误</param>
        public Output(string all, string stdOutput, string stdError = "")
        {
            All = all;
            Out = stdOutput;
            Error = stdError;
            LineAll = all.Split(Environment.NewLine.ToCharArray());
            LineOut = stdOutput.Split(Environment.NewLine.ToCharArray());
            LineError = stdError.Split(Environment.NewLine.ToCharArray());
        }

        /// <summary>
        /// 以可定义的tag或发送者,发送log
        /// </summary>
        /// <param name="tagOrSender"></param>
        /// <param name="printOnRelease"></param>
        public virtual void PrintOnLog(object tagOrSender, bool printOnRelease = false)
        {
            if (printOnRelease)
            {
                Logger.Info(tagOrSender, $"{this.GetType().Name}.PrintOnLog():{Environment.NewLine}{ToString()}");
            }
            else
            {
                Logger.Debug(tagOrSender, $"{this.GetType().Name}.PrintOnLog():{Environment.NewLine}{ToString()}");
            }
        }

        /// <summary>
        /// 以可定义的tag或发送者,打印在控制台
        /// </summary>
        /// <param name="tagOrSender"></param>
        public virtual void PrintOnConsole(object tagOrSender)
        {
            Console.WriteLine($"{tagOrSender} {this.GetType().Name}.PrintOnConsole():{Environment.NewLine}{ToString()}");
        }

        /// <summary>
        /// 打印到控制台
        /// </summary>
        [Obsolete("Plz use PrintOnConsole(bool printOnRelease=false) to instead", true)]
        public void PrintOnConsole()
        {
            Console.WriteLine($"{this.GetType().Name}.PrintOnConsole(): {this.ToString()}");
        }

        /// <summary>
        /// 打印到log
        /// </summary>
        /// <param name="printOnRelease"></param>
        [Obsolete("Plz use PrintOnLog(object tagOrSender,bool printOnRelease=false) to instead", true)]
        public void PrintOnLog(bool printOnRelease = false)
        {
            if (printOnRelease)
            {
                Logger.Info(this, $"PrintOnLog():{Environment.NewLine}{ToString()}");
            }
            else
            {
                Logger.Debug(this, $"PrintOnLog():{Environment.NewLine}{ToString()}");
            }
        }
    }
}
