using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util;
using AutumnBox.Logging.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Views.Windows
{
    /// <summary>
    /// LogEasyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogEasyWindow : Window
    {
        public LogEasyWindow()
        {
            InitializeComponent();
            var logMg = this.GetComponent<ILoggingManager>();
            logMg.Logs.All((log) =>
            {
                logBox.AppendText(log.ToFormatedString() + "\n");
                return true;
            });
            logBox.ScrollToEnd();
            logMg.Logs.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (ILog item in e.NewItems)
                    {
                        this.Dispatcher.Invoke(() => this.logBox.AppendText(item.ToFormatedString() + "\n"));
                    }
                    App.Current.Dispatcher.Invoke(() => logBox.ScrollToEnd());
                }
            };
        }
    }
}
