using SaleManagementSystem.Model.control;
using SaleManagementSystem.Model.View.Control;
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
using WpfApp;

namespace SaleManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int gridlength { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Width1 _width = new Width1()
            {
                GridLength1 = 150,
            };
            this.DataContext = _width;
            CC.Content = new Dashboard();
        }
        private bool menuExpand = true;
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Width1 _width = new Width1();
            if (!menuExpand)
            {
                MenuContainer.Width += 120;
                _width.GridLength1 = 150;
                menuExpand = true;

            }
            else
            {
                MenuContainer.Width -= 120;
                _width.GridLength1 = 30;
                menuExpand = false;
            }
            DataContext = _width;
        }

        private void btnMenuPage_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Dashboard();
        }

        private void btn_Products_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Products();
        }
    }
    class Width1
    {
        public Width1() { }
        public int GridLength1 { get; set; }
    }
}
