using SaleManagementSystem.Model.Repositories;
using SaleManagementSystem.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SaleManagementSystem.Model.View.Control
{
    public partial class Statistic : UserControl
    {
        public Statistic()
        {
            InitializeComponent();
            LoadStatisticTypes();
        }

        private void LoadStatisticTypes()
        {
            cbxReportType.ItemsSource = new List<string> { "Revenue", "Product Sales" };
            cbxReportType.SelectedIndex = 0;
        }

        private void cbxReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RevenuePanel.Visibility = Visibility.Collapsed;
            ProductPanel.Visibility = Visibility.Collapsed;

            switch (cbxReportType.SelectedValue.ToString())
            {
                case "Revenue":
                    RevenuePanel.Visibility = Visibility.Visible;
                    break;
                case "Product Sales":
                    ProductPanel.Visibility = Visibility.Visible;
                    dgProductList.ItemsSource = StatisticRepository.Instance.GetProductList();
                    break;
            }
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            cbxReportType_SelectionChanged(null, null);
        }

        private void btnFilterRevenue_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.MaxValue;
            txtRevenueSummary.Text = $"Total Revenue: {StatisticRepository.Instance.GetRevenueByDate(startDate, endDate):C}";
            dgRevenueOrders.ItemsSource = StatisticRepository.Instance.GetCompletedOrdersByDate(startDate, endDate);
        }

        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearchProduct.Text.ToLower();
            dgProductList.ItemsSource = string.IsNullOrWhiteSpace(keyword)
                ? StatisticRepository.Instance.GetProductList()
                : StatisticRepository.Instance.GetProductList().Where(p => p.Name.ToLower().Contains(keyword)).ToList();
        }

        private void dgProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProductList.SelectedItem is ProductView selectedProduct)
            {
                dgProductOrders.ItemsSource = StatisticRepository.Instance.GetOrdersByProduct(selectedProduct.ProductID, dpStartDate.SelectedDate, dpEndDate.SelectedDate);
            }
        }
    }
}
