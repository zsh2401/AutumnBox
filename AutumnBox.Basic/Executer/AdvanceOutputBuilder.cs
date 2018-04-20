/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 21:38:41 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 高级输出构建器
    /// </summary>
    public class AdvanceOutputBuilder : OutputBuilder
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int? ExitCode { get; set; } = null;

        /// <summary>
        /// 获取结果
        /// </summary>
        public new AdvanceOutput Result
        {
            get
            {
                return new AdvanceOutput(this.ToOutput(), ExitCode??24010);
            }
        }

        /// <summary>
        /// 清空构造器
        /// </summary>
        public override void Clear() {
            base.Clear();
            ExitCode = null;
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
