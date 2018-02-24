using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// UserManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagerWindow : Window
    {
        private readonly UserManager userManager;
        public UserManagerWindow(DeviceSerial device)
        {
            InitializeComponent();
            userManager = new UserManager(device);
            ButtonRemove.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private async void Refresh()
        {
            var users = await Task.Run(() =>
            {
                return userManager.GetUsers();
            });
            ListUsers.ItemsSource = users.Where((user) =>
            {
                return user.Id != 0;
            });
        }

        private void ListUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListUsers.SelectedIndex != -1)
            {
                TBUserName.Text = (ListUsers.SelectedItem as User).Name;
                TBUid.Text = (ListUsers.SelectedItem as User).Id.ToString();
                ButtonRemove.Visibility = Visibility.Visible;
            }
            else
            {
                TBUserName.Text = "...";
                TBUid.Text = "...";
                ButtonRemove.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            userManager.RemoveUser((ListUsers.SelectedItem as User).Id);
            Refresh();
        }
    }
}
