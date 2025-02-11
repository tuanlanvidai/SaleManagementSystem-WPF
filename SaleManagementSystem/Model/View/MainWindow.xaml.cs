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
        private bool menuExpand;
        public MainWindow()
        {
            InitializeComponent();
            menuExpand = true;
            ChangeMenuNav();
            CC.Content = new Dashboard();
        }
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
                ChangeMenuNav();
            }
            else
            {
                WindowState = WindowState.Maximized;
                ChangeMenuNav();
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
           ChangeMenuNav(ref menuExpand);
           
        }
        private void ChangeMenuNav(ref bool menu)
        {
            Width1 _width = new Width1();
            if (WindowState == WindowState.Maximized)
            {
                if (menu)
                {
                    _width.GridLength1 = 250;
                    menu = false;

                }
                else
                {
                    _width.GridLength1 = 30;
                    menu = true;
                }
            }
            else
            {
                if (menu)
                {
                    _width.GridLength1 = 150;
                    menu = false;

                }
                else
                {
                    _width.GridLength1 = 30;
                    menu = true;
                }
            }
            DataContext = _width;
        }

        private void ChangeMenuNav()
        {
            Width1 _width = new Width1();
            if (WindowState == WindowState.Maximized)
            {
                if (!menuExpand)
                {
                    _width.GridLength1 = 200;

                }
                else
                {
                    _width.GridLength1 = 30;
                }
            }
            else
            {
                if (!menuExpand)
                {
                    _width.GridLength1 = 150;

                }
                else
                {
                    _width.GridLength1 = 30;
                }
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
        private void btn_Statistic_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new Statistic(); 
        }
    }
    class Width1
    {
        public Width1() { }
        public int GridLength1 { get; set; }
    }
}
