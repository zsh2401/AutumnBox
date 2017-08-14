using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AutumnBox.UI
{
    /// <summary>
    /// 进度条
    /// </summary>
    public partial class CustomBar : UserControl
    {
        //集成到按指定时间间隔和指定优先级处理的 System.Windows.Threading.Dispatcher 队列中的计时器。
        private DispatcherTimer animationTimer;
        private CustomBarDataModel _dataModel;
        private int index = 0;
        #region 构造方法与加载
        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomBar()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 加载后刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBarControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher);
            //指定时间间隔
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, TimeSpan);
            if (EllipseCount < 1)
            {
                EllipseCount = 12;
            }
            for (int i = 0; i < EllipseCount; i++)
            {
                ProgressBarCanvas.Children.Add(new Ellipse());
            }
            var dataModel = new CustomBarDataModel()
            {
                CanvasSize = CanvasSize,
                EclipseSize = EllipseSize
            };
            _dataModel = dataModel;
            this.DataContext = dataModel;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置圆圈数量
        /// 默认12
        /// </summary>
        public double EllipseCount
        {
            get { return (double)GetValue(EllipseCountProperty); }
            set { SetValue(EllipseCountProperty, value); }
        }
        public static readonly DependencyProperty EllipseCountProperty =
            DependencyProperty.Register("EllipseCount", typeof(double), typeof(CustomBar),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 获取或设置圆圈大小
        /// 默认10
        /// </summary>
        public double EllipseSize
        {
            get { return (double)GetValue(EllipseSizeProperty); }
            set { SetValue(EllipseSizeProperty, value); }
        }
        public static readonly DependencyProperty EllipseSizeProperty =
            DependencyProperty.Register("EllipseSize", typeof(double), typeof(CustomBar),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 获取或设置面板大小
        /// 默认80
        /// </summary>
        public double CanvasSize
        {
            get { return (double)GetValue(CanvasSizeProperty); }
            set { SetValue(CanvasSizeProperty, value); }
        }
        public static readonly DependencyProperty CanvasSizeProperty =
            DependencyProperty.Register("CanvasSize", typeof(double), typeof(CustomBar),
            new FrameworkPropertyMetadata(80.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 获取或设置每次旋转角度
        /// 默认10.0
        /// </summary>
        public double StepAngle
        {
            get { return (double)GetValue(StepAngleProperty); }
            set { SetValue(StepAngleProperty, value); }
        }

        public static readonly DependencyProperty StepAngleProperty =
            DependencyProperty.Register("StepAngle", typeof(double), typeof(CustomBar),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 获取或设置每次旋转间隔时间（毫秒）
        /// 默认100毫秒
        /// </summary>
        public int TimeSpan
        {
            get { return (int)GetValue(TimeSpanProperty); }
            set { SetValue(TimeSpanProperty, value); }
        }
        public static readonly DependencyProperty TimeSpanProperty =
            DependencyProperty.Register("TimeSpan", typeof(int), typeof(CustomBar),
            new FrameworkPropertyMetadata(100, FrameworkPropertyMetadataOptions.AffectsRender));
       
        #endregion

        #region 方法
        /// <summary>
        /// Canvas加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            //设置设置圆的位置和旋转角度
            SetEclipsePosition(_dataModel);
            //DesignerProperties   提供用于与设计器进行通信的附加属性。
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (this.Visibility == System.Windows.Visibility.Visible)
                {
                    //超过计时器间隔时发生。
                    animationTimer.Tick += HandleAnimationTick;
                    animationTimer.Start();
                }
            }
        }

        /// <summary>
        /// 设置圆的位置和旋转角度
        /// </summary>
        private void SetEclipsePosition(CustomBarDataModel dataModel)
        {
            //圆周长就是：C = π * d 或者C=2*π*r（其中d是圆的直径，r是圆的半径）
            double r =dataModel.R;

            var children=ProgressBarCanvas.Children;
            int count = children.Count;
            double step = (Math.PI * 2) / count;

            //根据圆中正弦、余弦计算距离
            int index = 0;
            foreach (var element in children)
            {
                var ellipse = element as Ellipse;
                //透明度
                var opacity = Convert.ToDouble(index)/(count - 1);
                ellipse.SetValue(UIElement.OpacityProperty, opacity<0.05?0.05:opacity);
                //距离
                double left = r + Math.Sin(step*index)*r;
                ellipse.SetValue(Canvas.LeftProperty,left);
                double top = r - Math.Cos(step*index)*r;
                ellipse.SetValue(Canvas.TopProperty, top);

                index++;
            }
        }

        /// <summary>
        /// Canvas卸载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            animationTimer.Stop();
            //除去委托
            animationTimer.Tick -= HandleAnimationTick;
        }

        /// <summary>
        /// 超过计时器间隔时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleAnimationTick(object sender, EventArgs e)
        {
            //设置旋转角度
            SpinnerRotate.Angle = (SpinnerRotate.Angle + StepAngle) % 360;
        }
        #endregion
    }
}
