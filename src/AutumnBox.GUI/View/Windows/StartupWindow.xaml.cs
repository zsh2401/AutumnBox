using AutumnBox.GUI.Util.Loader;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// StartupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartupWindow
    {
        private readonly AbstractAppLoader appLoader;
        public StartupWindow()
        {
            InitializeComponent();
            appLoader = (AbstractAppLoader)new ObjectBuilder(typeof(GeneralAppLoader),App.Current.Lake).Build();
            appLoader.StepFinished += AppLoader_StepFinished;
            appLoader.Succeced += AppLoader_Succeced;
            appLoader.Failed += AppLoader_Failed;
        }

        private void AppLoader_Failed(object sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Failed");
            });
        }

        private void AppLoader_Succeced(object sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                SLogger<StartupWindow>.Info("Switching to MainWindowV3");
                var mainWindow = new MainWindowV3();
                App.Current.MainWindow = mainWindow;
                this.Close();
                mainWindow.Show();
                SLogger<StartupWindow>.Info("Switched");
            });
        }

        private void AppLoader_StepFinished(object sender, StepFinishedEventArgs e)
        {
            double progress = 100.0 * e.FinishedStep / e.TotalStepCount;
            SLogger<StartupWindow>.Info($"{e.FinishedStep} / {e.TotalStepCount}");
            App.Current.Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = progress;
            });
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            _ = appLoader.LoadAsync();
        }
    }
}
