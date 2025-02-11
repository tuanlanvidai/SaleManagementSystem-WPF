using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SaleManagementSystem.Model.View.Control
{
    public partial class Statistic : UserControl
    {
        public Statistic()
        {
            InitializeComponent();
            LoadStatistics("All Products"); // Hiển thị mặc định
        }

        private void cbxReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxReportType.SelectedItem is ComboBoxItem selectedItem)
            {
                string reportType = selectedItem.Content.ToString();
                LoadStatistics(reportType);
            }
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            if (cbxReportType.SelectedItem is ComboBoxItem selectedItem)
            {
                string reportType = selectedItem.Content.ToString();
                LoadStatistics(reportType);
            }
        }

        private void LoadStatistics(string type)
        {
            List<StatisticData> statistics = new List<StatisticData>();
            Random random = new Random();

            switch (type)
            {
                case "All Products":
                    for (int i = 1; i <= 5; i++)
                    {
                        statistics.Add(new StatisticData
                        {
                            Category = "Product",
                            Name = $"Product {i}",
                            SalesCount = random.Next(50, 200),
                            TotalRevenue = random.Next(5000, 50000)
                        });
                    }
                    txtSummary.Text = "Summary: Overall product sales";
                    break;

                case "Per Product":
                    for (int i = 1; i <= 5; i++)
                    {
                        statistics.Add(new StatisticData
                        {
                            Category = "Product Detail",
                            Name = $"Product {i}",
                            SalesCount = random.Next(10, 100),
                            TotalRevenue = random.Next(1000, 10000)
                        });
                    }
                    txtSummary.Text = "Summary: Sales per product";
                    break;

                case "Revenue":
                    for (int i = 1; i <= 12; i++)
                    {
                        statistics.Add(new StatisticData
                        {
                            Category = "Revenue",
                            Name = $"Month {i}",
                            SalesCount = 0,
                            TotalRevenue = random.Next(50000, 200000)
                        });
                    }
                    txtSummary.Text = "Summary: Monthly revenue data";
                    break;

                case "Employee Sales":
                    for (int i = 1; i <= 3; i++)
                    {
                        statistics.Add(new StatisticData
                        {
                            Category = "Employee",
                            Name = $"Employee {i}",
                            SalesCount = random.Next(5, 50),
                            TotalRevenue = random.Next(5000, 50000)
                        });
                    }
                    txtSummary.Text = "Summary: Sales by employees";
                    break;
            }

            dgStatistics.ItemsSource = statistics;
        }
    }

    public class StatisticData
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
