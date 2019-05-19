using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutumnBox.GUI.View.Controls
{
    public class SliceView : UserControl
    {
        private readonly Stack<object> viewStack = new Stack<object>();
        public SliceView(object baseView)
        {
            if (baseView == null)
            {
                throw new ArgumentNullException(nameof(baseView));
            }
            Next(baseView, null);
        }
        public int StackCount()
        {
            return viewStack.Count;
        }
        public void Back()
        {
            viewStack.Pop();
            UpdateView();
        }
        public void Next(object view, string title = null)
        {
            viewStack.Push(GenerateView(view, title));
            UpdateView();
        }
        public object GenerateView(object view, string title)
        {
            string _title = App.Current.Resources["title"]?.ToString() ?? title;
            DockPanel _baseDock = new DockPanel();

            if (StackCount() != 0)
            {
                //初始化标题栏
                StackPanel _navBar = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                Button backBtn = new Button();
                TextBlock titleTB = new TextBlock() { FontSize = 23, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(20, 0, 0, 0), Text = _title };
                IconElement.SetGeometry(backBtn, (Geometry)App.Current.Resources["LeftGeometry"]);
                backBtn.Click += (s, e) => Back();
                _navBar.Children.Add(backBtn);
                _navBar.Children.Add(titleTB);
                _baseDock.Children.Add(_navBar);
                DockPanel.SetDock(_navBar, Dock.Top);
            }
            //初始化内容容器
            ContentControl contentControl = new ContentControl()
            {
                Content = view
            };
            _baseDock.Children.Add(contentControl);
            DockPanel.SetDock(contentControl, Dock.Bottom);

            return _baseDock;
        }
        private void UpdateView()
        {
            Content = viewStack.Peek();
        }
    }
}
