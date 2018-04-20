/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 17:44:42 (UTC +8:00)
** desc： ...
*************************************************/
using System.Text;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 输出构造器
    /// </summary>
    public class OutputBuilder
    {
        private StringBuilder outSb;
        private StringBuilder errSb;
        private StringBuilder allSb;
        /// <summary>
        /// 构建一个新的OutputBuilder实例
        /// </summary>
        public OutputBuilder()
        {
            outSb = new StringBuilder();
            errSb = new StringBuilder();
            allSb = new StringBuilder();
        }

        /// <summary>
        /// 添加一段标准输出
        /// </summary>
        /// <param name="text"></param>
        public void AppendOut(string text)
        {
            outSb.AppendLine(text);
            allSb.AppendLine(text);
            LeastLine = text;
        }

        /// <summary>
        /// 添加一段标准错误
        /// </summary>
        /// <param name="text"></param>
        public void AppendError(string text)
        {
            errSb.AppendLine(text);
            allSb.AppendLine(text);
            LeastLine = text;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="builder"></param>
        public void Append(OutputBuilder builder) {
            outSb.Append(builder.outSb);
            errSb.Append(builder.errSb);
            allSb.Append(builder.allSb);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="output"></param>
        public void Append(Output output) {
            outSb.Append(output.Out);
            errSb.Append(output.Error);
            allSb.Append(output.All);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            outSb.Clear();
            errSb.Clear();
            allSb.Clear();
        }

        /// <summary>
        /// 监听一个IOutputable的输出并记录
        /// </summary>
        /// <param name="sender"></param>
        public void Register(IOutputable sender)
        {
            sender.OutputReceived += Sender_OutputReceived;
        }

        /// <summary>
        /// 取消监听一个IOutputable
        /// </summary>
        /// <param name="sender"></param>
        public void Unregister(IOutputable sender)
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
            return new Output(allSb.ToString(), outSb.ToString(), errSb.ToString());
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
