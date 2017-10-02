using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox
{
    public partial class Window1
    {
        private RateBox rateBox;
        /// <summary>
        /// 通过此方法来显示进度框可以确保不会同时出现多个进度窗口
        /// </summary>
        private void ShowRateBox(RunningManager rm = null)
        {
            try
            {
                if (rm == null)
                {
                    rateBox = new RateBox(this);
                    rateBox.ShowDialog();
                    return;
                }
                if (rateBox.IsActive) rateBox.Close();
                rateBox = new RateBox(this, rm);
                rateBox.ShowDialog();
            }
            catch
            {
                this.rateBox = new RateBox(this, rm);
                rateBox.ShowDialog();
            }
        }
        /// <summary>
        /// 隐藏进度窗口
        /// </summary>
        private void HideRateBox()
        {
            try
            {
                this.rateBox.Close();
            }
            catch { }
        }
    }
}
