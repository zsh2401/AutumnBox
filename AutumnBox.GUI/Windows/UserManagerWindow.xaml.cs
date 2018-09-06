using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// UserManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagerWindow : Window
    {
        private readonly UserManager userManager;
        public UserManagerWindow(IDevice device)
        {
            InitializeComponent();
            //userManager = new UserManager(device);
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
