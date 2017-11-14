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
    using AutumnBox.Basic.Function.Event;
    using System.Collections.Generic;
    using System.Text;

    public sealed class OutputData
    {
        public IOutSender OutSender
        {
            set
            {
                if (_OutSender != null)
                {
                    throw new EventAddException("Only set one!");
                }
                else
                {
                    _OutSender = value;
                    _OutSender.OutputReceived += (s, e) =>
                    {
                        if (!e.IsError)
                            OutAdd(e.Text);
                        else
                            ErrorAdd(e.Text);
                    };
                }
            }
        }
        private IOutSender _OutSender = null;
        public List<string> LineAll { get; private set; } = new List<string>();
        public List<string> LineOut { get; private set; } = new List<string>();
        public List<string> LineError { get; private set; } = new List<string>();
        public StringBuilder All { get; private set; } = new StringBuilder();
        public StringBuilder Out { get; private set; } = new StringBuilder();
        public StringBuilder Error { get; private set; } = new StringBuilder();
        private bool _IsClosed = false;
        /// <summary>
        /// 添加输出信息
        /// </summary>
        /// <param name="outData"></param>
        public void OutAdd(string outData)
        {
            if (outData == null) return;
            if (_IsClosed) return;
            All.Append(outData + System.Environment.NewLine);
            LineAll.Add(outData);
            LineOut.Add(outData);
            Out.Append(outData + System.Environment.NewLine);
        }
        /// <summary>
        /// 添加错误输出信息
        /// </summary>
        /// <param name="errorData"></param>
        public void ErrorAdd(string errorData)
        {
            if (errorData == null) return;
            if (_IsClosed) return;
            All.Append(errorData + System.Environment.NewLine);
            LineAll.Add(errorData);
            LineError.Add(errorData);
            Error.Append(errorData + System.Environment.NewLine);
        }
        /// <summary>
        /// 添加另一个OutputData对象的内容
        /// </summary>
        /// <param name="output"></param>
        public void Append(OutputData output)
        {
            if (_IsClosed) return;
            LineAll.AddRange(output.LineAll);
            LineOut.AddRange(output.LineOut);
            LineError.AddRange(output.LineError);
            All.Append(output.ToString());
            Out.Append(output.Out.ToString());
            Error.Append(output.Error.ToString());
        }
        /// <summary>
        /// 清空内容
        /// </summary>
        public void Clear()
        {
            Out = new StringBuilder();
            Error = new StringBuilder();
            All = new StringBuilder();
            LineAll.Clear();
            LineOut.Clear();
            LineError.Clear();
        }
        /// <summary>
        /// 停止添加内容,执行后将不可再添加内容
        /// </summary>
        public void StopAdding()
        {
            _IsClosed = true;
        }
        /// <summary>
        /// 获取完整的输出数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return All.ToString();
        }
    }
}
