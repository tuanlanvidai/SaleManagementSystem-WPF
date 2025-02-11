using SaleManagementSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementSystem.Model.ModelView
{
    internal class StatisticView
    {
        public int ID { get; set; } = 0;  // ID của thống kê
        public string Type { get; set; } = "";  // Loại thống kê (Doanh thu, Sản phẩm, Nhân viên...)
        public string Target { get; set; } = "";  // Đối tượng thống kê (Nhân viên, Sản phẩm...)
        public int TargetID { get; set; } = 0;  // ID của đối tượng
        public string TimePeriod { get; set; } = "";  // Thời gian (tháng, quý, năm)
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public string Metric { get; set; } = "";  // Chỉ số thống kê
        public decimal Value { get; set; } = 0;  // Giá trị thống kê
        public DateTime LastUpdated { get; set; } = DateTime.Now; // Thời gian cập nhật cuối

        // Chuyển đổi từ `Tbl_Statistics` sang `StatisticView`
        public static StatisticView ToStatisticView(Tbl_Statistics item)
        {
            return new StatisticView
            {
                ID = item.statistic_id,
                Type = GetStatisticTypeName(item.type_id ?? 0),
                Target = item.statistic_target ?? "Unknown",
                TargetID = item.target_id ?? 0,
                TimePeriod = item.time_period ?? "Unknown",
                StartDate = item.start_date ?? DateTime.MinValue,
                EndDate = item.end_date ?? DateTime.MinValue,
                Metric = item.metric_name ?? "Unknown",
                Value = item.value ?? 0,
                LastUpdated = item.last_updated ?? DateTime.Now
            };
        }

        // Chuyển đổi từ `StatisticView` sang `Tbl_Statistics`
        public static Tbl_Statistics ToStatistic(StatisticView item)
        {
            return new Tbl_Statistics
            {
                statistic_id = item.ID,
                type_id = GetTypeID(item.Type),
                statistic_target = item.Target,
                target_id = item.TargetID,
                time_period = item.TimePeriod,
                start_date = item.StartDate,
                end_date = item.EndDate,
                metric_name = item.Metric,
                value = item.Value,
                last_updated = item.LastUpdated
            };
        }

        // Hàm lấy ID loại thống kê từ tên loại
        private static int GetTypeID(string typeName)
        {
            switch (typeName)
            {
                case "Revenue": return 1;
                case "Product Sales": return 2;
                case "Employee Sales": return 3;
                case "Stock Report": return 4;
                case "Total Orders": return 5;
                default: return 0;
            }
        }

        // Hàm lấy tên loại thống kê từ ID
        private static string GetStatisticTypeName(int typeId)
        {
            switch (typeId)
            {
                case 1: return "Revenue";
                case 2: return "Product Sales";
                case 3: return "Employee Sales";
                case 4: return "Stock Report";
                case 5: return "Total Orders";
                default: return "Unknown";
            }
        }
    }
}
