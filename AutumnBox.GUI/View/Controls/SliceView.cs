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
        private readonly ContentControl contentContainer = new ContentControl();
        private readonly Grid navBar = new Grid();
        private readonly Button backButton = new Button();
        private readonly DockPanel baseDock = new DockPanel();
        public SliceView(object baseView)
        {
            if (baseView == null)
            {
                throw new ArgumentNullException(nameof(baseView));
            }
            IconElement.SetGeometry(backButton, (Geometry)App.Current.Resources["LeftGeometry"]);
            backButton.Click += (s, e) => Back();
            backButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            navBar.Children.Add(backButton);
            navBar.Visibility = System.Windows.Visibility.Hidden;

            baseDock.Children.Add(navBar);
            baseDock.Children.Add(contentContainer);

            DockPanel.SetDock(contentContainer, Dock.Bottom);
            DockPanel.SetDock(navBar, Dock.Top);
            Content = baseDock;
            Next(baseView);
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
        public void Next(object view)
        {
            viewStack.Push(view);
            UpdateView();
        }
        private void UpdateView()
        {
            contentContainer.Content = viewStack.Peek();
            if (StackCount() == 1)
                navBar.Visibility = System.Windows.Visibility.Hidden;
            else
                navBar.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
