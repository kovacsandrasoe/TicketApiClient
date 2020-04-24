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
using TicketApiClient.Models;

namespace TicketApiClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RestService<LoginViewModel, string> loginservice = new RestService<LoginViewModel, string>(
                "https://localhost:44346/", "/login");

            LoginResponseViewModel lrvm = null;

            try
            {
                lrvm = await loginservice.Post<LoginResponseViewModel>(new LoginViewModel()
                {
                    Username = tb_user.Text,
                    Password = tb_pass.Password
                });

                MainWindow mw = new MainWindow(lrvm.Token);
                this.Visibility = Visibility.Hidden;
                mw.ShowDialog();
                this.Close();

            }
            catch (System.Net.Http.HttpRequestException re)
            {
                MessageBox.Show(re.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
}
