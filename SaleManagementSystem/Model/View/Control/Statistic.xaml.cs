using SaleManagementSystem.Model.Entities;
using SaleManagementSystem.Model.Reponsitories;
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

        // Lấy danh sách loại thống kê từ database và đổ vào ComboBox
        private void LoadStatisticTypes()
        {
            var statisticTypes = StatisticRepository.Instance.GetStatisticTypes();

            if (statisticTypes == null || statisticTypes.Count == 0)
            {
                MessageBox.Show("No statistic types found in database!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                cbxReportType.ItemsSource = statisticTypes;
                cbxReportType.SelectedIndex = 0;
                Console.WriteLine("ComboBox Data Set: " + string.Join(", ", statisticTypes));
            }
        }

        private void cbxReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxReportType.SelectedValue is string reportType)
            {
                LoadStatistics(reportType);
            }
        }
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            if (cbxReportType.SelectedValue is string reportType)
            {
                LoadStatistics(reportType);
            }
        }
        private void LoadStatistics(string type)
        {
            var statistics = StatisticRepository.Instance.GetStatisticsByType(type).ToList();
            dgStatistics.ItemsSource = statistics;

            // Hiển thị tổng quan
            txtSummary.Text = $"Summary: {type} statistics";
        }
    }
    }
