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
namespace AutumnBox.Basic.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 输出封装类
    /// </summary>
    public class Output : IEquatable<Output>
    {
        /// <summary>
        /// 空输出，此字段为只读
        /// </summary>
        public static readonly Output Empty = new Output(String.Empty, String.Empty, String.Empty);
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
            Out = "";
            Error = "";
            All = "";
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
        /// 比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Output);
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Output other)
        {
            return other != null &&
                   //EqualityComparer<string[]>.Default.Equals(LineAll, other.LineAll) &&
                   //EqualityComparer<string[]>.Default.Equals(LineOut, other.LineOut) &&
                   //EqualityComparer<string[]>.Default.Equals(LineError, other.LineError) &&
                   All == other.All &&
                   Out == other.Out &&
                   Error == other.Error;
        }

        /// <summary>
        /// 获取HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -1661239530;
            //hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(LineAll);
            //hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(LineOut);
            //hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(LineError);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(All);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Out);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Error);
            return hashCode;
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="output1"></param>
        /// <param name="output2"></param>
        /// <returns></returns>
        public static bool operator ==(Output output1, Output output2)
        {
            return EqualityComparer<Output>.Default.Equals(output1, output2);
        }

        /// <summary>
        /// 不等
        /// </summary>
        /// <param name="output1"></param>
        /// <param name="output2"></param>
        /// <returns></returns>
        public static bool operator !=(Output output1, Output output2)
        {
            return !(output1 == output2);
        }
    }
}
