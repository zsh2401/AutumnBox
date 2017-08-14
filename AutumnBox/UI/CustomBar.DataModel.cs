using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.UI
{
    /// <summary>
    /// 进度条Model类
    /// </summary>
    public class CustomBarDataModel
    {
        public double EclipseSize { get; set; }
        public double CanvasSize { get; set; }
        public double ViewBoxSize
        {
            get
            {
                double length = Convert.ToDouble(CanvasSize) - Convert.ToDouble(EclipseSize);
                return length;
            }
        }
        public double EclipseLeftLength
        {
            get
            {
                double length = Convert.ToDouble(CanvasSize) / 2;
                return length;
            }
        }
        public double R
        {
            get
            {
                double length = (Convert.ToDouble(CanvasSize) - Convert.ToDouble(EclipseSize)) / 2;
                return length;
            }
        }
    }
}
