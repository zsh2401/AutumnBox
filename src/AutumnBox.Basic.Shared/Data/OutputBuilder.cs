/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 17:44:42 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Text;

namespace AutumnBox.Basic.Data
{
    /// <summary>
    /// 输出构造器
    /// </summary>
    public class OutputBuilder
    {
        private readonly StringBuilder sbOutput;
        private readonly StringBuilder sbError;
        private readonly StringBuilder sbAll;
        /// <summary>
        /// 构建一个新的OutputBuilder实例
        /// </summary>
        public OutputBuilder()
        {
            sbOutput = new StringBuilder();
            sbError = new StringBuilder();
            sbAll = new StringBuilder();
        }

        /// <summary>
        /// 添加一段标准输出
        /// </summary>
        /// <param name="text"></param>
        public void AppendOut(string text)
        {
            sbOutput.AppendLine(text);
            sbAll.AppendLine(text);
            LeastLine = text;
        }

        /// <summary>
        /// 添加一段标准错误
        /// </summary>
        /// <param name="text"></param>
        public void AppendError(string text)
        {
            sbError.AppendLine(text);
            sbAll.AppendLine(text);
            LeastLine = text;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="builder"></param>
        public void Append(OutputBuilder builder)
        {
            sbOutput.Append(builder.sbOutput);
            sbError.Append(builder.sbError);
            sbAll.Append(builder.sbAll);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="output"></param>
        public void Append(Output output)
        {
            sbOutput.Append(output.Out);
            sbError.Append(output.Error);
            sbAll.Append(output.All);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            sbAll.Clear();
            sbError.Clear();
            sbOutput.Clear();
        }

        /// <summary>
        /// 接收器Action
        /// </summary>
        public Action<OutputReceivedEventArgs> Receiver
        {
            get => (e) =>
            {
                if (e.IsError)
                {
                    AppendError(e.Text);
                }
                else
                {
                    AppendOut(e.Text);
                }
            };
        }

        /// <summary>
        /// 监听一个IOutputable的输出并记录
        /// </summary>
        /// <param name="sender"></param>
        public void Register(INotifyOutput sender)
        {
            sender.OutputReceived += Sender_OutputReceived;
        }

        /// <summary>
        /// 取消监听一个IOutputable
        /// </summary>
        /// <param name="sender"></param>
        public void Unregister(INotifyOutput sender)
        {
            sender.OutputReceived -= Sender_OutputReceived;
        }

        private void Sender_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (e.IsError)
            {
                AppendError(e.Text);
            }
            else
            {
                AppendOut(e.Text);
            }
        }

        /// <summary>
        /// 最新的一行输出
        /// </summary>
        public string LeastLine { get; private set; }

        /// <summary>
        /// 获取结果,建议使用Result属性
        /// </summary>
        /// <returns></returns>
        public Output ToOutput()
        {
            return new Output(sbAll.ToString(), sbOutput.ToString(), sbError.ToString());
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public Output Result
        {
            get
            {
                return ToOutput();
            }
        }

        /// <summary>
        /// 字符化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Result.ToString();
        }
    }
}
