using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.View.Windows;
using AutumnBox.GUI.ViewModel;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.View.Controls
{
    /// <summary>
    /// Extensions.xaml 的交互逻辑
    /// </summary>
    public partial class ExtensionsWrapPanel : UserControl
    {
        #region WW
        public class WrapperWrapper
        {
            public string ToolTip
            {
                get
                {
                    return Wrapper.Info.Name + System.Environment.NewLine +
                         Wrapper.Info.FormatedDesc;
                }
            }
            public IExtensionWrapper Wrapper { get; private set; }
            public string Name => Wrapper.Info.Name;
            public ImageSource Icon
            {
                get
                {
                    if (icon == null) LoadIcon();
                    return icon;
                }
            }
            private ImageSource icon;
            private WrapperWrapper(IExtensionWrapper wrapper)
            {
                this.Wrapper = wrapper;
            }
            private void LoadIcon()
            {
                if (Wrapper.Info.Icon == null)
                {
                    icon = App.Current.Resources["DefaultExtensionIcon"] as ImageSource;
                }
                else
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = new MemoryStream(Wrapper.Info.Icon);
                    bmp.EndInit();
                    bmp.Freeze();
                    icon = bmp;
                }
            }
            public static IEnumerable<WrapperWrapper> From(IEnumerable<IExtensionWrapper> wrappers)
            {
                List<WrapperWrapper> result = new List<WrapperWrapper>();
                foreach (var wrapper in wrappers)
                {
                    result.Add(new WrapperWrapper(wrapper));
                }
                return result;
            }
        }
        #endregion

        private MVVM.FlexiableCommand CommandMouseClick
        {
            get { return (MVVM.FlexiableCommand)GetValue(CommandMouseClickProperty); }
            set { SetValue(CommandMouseClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandMouseClick.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty CommandMouseClickProperty =
            DependencyProperty.Register("CommandMouseClick", typeof(MVVM.FlexiableCommand), typeof(ExtensionsWrapPanel), new PropertyMetadata(null));


        public WrapperWrapper SelectedItem
        {

            get { return (WrapperWrapper)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Selected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(WrapperWrapper), typeof(ExtensionsWrapPanel), new PropertyMetadata(_SelectionChanged));

        private IEnumerable<WrapperWrapper> WExtensions
        {
            get { return (IEnumerable<WrapperWrapper>)GetValue(WExtensionsProperty); }
            set { SetValue(WExtensionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WExtensions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WExtensionsProperty =
            DependencyProperty.Register("WExtensions", typeof(IEnumerable<WrapperWrapper>), typeof(ExtensionsWrapPanel), new PropertyMetadata(null));


        public IEnumerable<IExtensionWrapper> Extensions
        {
            get { return (IEnumerable<IExtensionWrapper>)GetValue(ExtensionsProperty); }
            set
            {
                SetValue(ExtensionsProperty, value);
            }
        }
        // Using a DependencyProperty as the backing store for Extensions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtensionsProperty =
            DependencyProperty.Register("Extensions", typeof(IEnumerable<IExtensionWrapper>),
                typeof(ExtensionsWrapPanel),
                new PropertyMetadata(ExtensionsChanged));


        public ExtensionsWrapPanel()
        {
            InitializeComponent();
            CommandMouseClick = new MVVM.FlexiableCommand((p) =>
            {
                switch (p as string)
                {
                    case "LeftClick":
                        SGLogger<ExtensionsWrapPanel>.Debug("click left");
                        break;
                    case "LeftDoubleClick":
                        LeftDoubleClickExtension();
                        break;
                    case "RightClick":
                    case "RightDoubleClick":
                    default:
                        break;
                }
            });
        }
        private void LeftDoubleClickExtension()
        {
            if (Settings.Default.ShowDetailOnDoubleClick)
            {
                bool? result = new ChoiceWindow()
                {
                    Title = "Notice",
                    Message = SelectedItem.Wrapper.Info.FormatedDesc,
                }.ShowDialog();
                if (result == true)
                {
                    SelectedItem.Wrapper.RunAsync(DeviceSelectionObserver.Instance.CurrentDevice);
                }
            }
            else
            {
                SelectedItem.Wrapper.RunAsync(DeviceSelectionObserver.Instance.CurrentDevice);
            }
        }
        private static void _SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var extWrapPanel = (ExtensionsWrapPanel)d;
            //extWrapPanel.SelectionChanged?.Invoke(d, new EventArgs());
        }
        private static void ExtensionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var extWrapPanel = (ExtensionsWrapPanel)d;
            extWrapPanel.SelectedItem = null;
            extWrapPanel.WExtensions = WrapperWrapper
                .From((IEnumerable<IExtensionWrapper>)e.NewValue);
        }
    }
}
