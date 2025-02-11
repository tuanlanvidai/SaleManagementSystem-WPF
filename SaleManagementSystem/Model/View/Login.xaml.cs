using SaleManagementSystem;
using SaleManagementSystem.Model.ModelView;
using SaleManagementSystem.Model.Reponsitories;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void LoginCheck()
        {
            List<UserView> users = UserRepository.Instance.GetUser(txtUsername.Text);
            if (users != null)
            {
                foreach (UserView user in users)
                {
                    if (txtPassword.Password == user.Password)
                    {
                        this.Visibility = Visibility.Hidden;
                        MainWindow main = new MainWindow();
                        main.Show();
                    }
                    else if (txtPassword.Password.Equals(""))
                    {
                        MessageBox.Show("Please Enter Password");
                        txtPassword.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password");
                        txtPassword.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           LoginCheck();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                LoginCheck();
            }
        }
    }
}
