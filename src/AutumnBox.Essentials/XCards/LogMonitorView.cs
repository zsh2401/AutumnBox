/*

* ==============================================================================
*
* Filename: LogMonitorView
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 16:51:24
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.Essentials.XCards
{
    [Component(SingletonMode = false, Type = typeof(LogMonitorView))]
    class LogMonitorView : IXCard
    {
        public int Priority => 0;

        public object View { get; private set; }

        private TextBox mainTextBox;

        public bool Registered { get; private set; }

        public void Create()
        {
            SLogger.Info(this, $"{GetHashCode()},creating");
            mainTextBox = new TextBox()
            {
                TextWrapping = System.Windows.TextWrapping.Wrap,
                FontSize = 9,
                IsReadOnly = true,
                IsReadOnlyCaretVisible = false
            };
            ScrollViewer sv = new ScrollViewer()
            {
                Content = mainTextBox,
                Height = 500,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };
            View = sv;
            InitializeContent().ConfigureAwait(true);
        }

        public async Task InitializeContent()
        {
            var text = await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                LoggingManager.CoreLogger.Logs.All((log) =>
                {
                    sb.AppendLine(MyFormat(log));
                    return true;
                });
                return sb.ToString();
            });
            mainTextBox.AppendText(text);
            LoggingManager.CoreLogger.Logs.CollectionChanged += Logs_CollectionChanged;
        }

        private string MyFormat(ILog log)
        {
            return $"{log.Category}/{log.Level}:{log.Message}";
        }

        private void Logs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (ILog newItem in e.NewItems)
                {
                    mainTextBox.Dispatcher.Invoke(() =>
                    {
                        mainTextBox.AppendText($"{MyFormat(newItem)}\n");
                    });
                }
            }
        }

        public void Destory()
        {
            View = null;
            mainTextBox = null;
            LoggingManager.CoreLogger.Logs.CollectionChanged -= Logs_CollectionChanged;
        }

        public void Update()
        {

        }

        ~LogMonitorView()
        {
            SLogger.Warn(this, "~");
        }

    }
}
