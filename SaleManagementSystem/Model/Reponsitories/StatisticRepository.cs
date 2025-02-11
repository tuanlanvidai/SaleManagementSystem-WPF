using SaleManagementSystem.Model.Entities;
using SaleManagementSystem.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementSystem.Model.Reponsitories
{
    internal sealed class StatisticRepository
    {
        private static StatisticRepository _instance = null;
        private StatisticRepository() { }

        public static StatisticRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StatisticRepository();
                }
                return _instance;
            }
        }

        //Lấy toàn bộ thống kê
        public HashSet<StatisticView> GetAll()
        {
            using (var db = new DbEntities())
            {
                return db.Tbl_Statistics
                    .Select(d => StatisticView.ToStatisticView(d))
                    .ToHashSet();
            }
        }

        //Lấy danh sách loại thống kê từ database (DÙNG CHO COMBOBOX)
        public List<string> GetStatisticTypes()
        {
            using (var db = new DbEntities())
            {
                var types = db.Tbl_StatisticTypes.Select(t => t.type_name).ToList();

                // 📌 Debug: In danh sách loại thống kê ra console
                Console.WriteLine("Statistic Types Found: " + string.Join(", ", types));

                return types;
            }
        }

        // Lấy thống kê theo loại (Doanh thu, Sản phẩm, Nhân viên)
        public HashSet<StatisticView> GetStatisticsByType(string typeName)
        {
            using (var db = new DbEntities())
            {
                int typeID = db.Tbl_StatisticTypes
                    .Where(t => t.type_name == typeName)
                    .Select(t => t.type_id)
                    .FirstOrDefault();

                var statistics = db.Tbl_Statistics
                    .Where(d => d.type_id == typeID)
                    .ToList();

                return statistics.Select(StatisticView.ToStatisticView).ToHashSet();
            }
        }

        // Lấy thống kê theo thời gian (tháng, quý, năm)
        public HashSet<StatisticView> GetStatisticsByTime(string timePeriod, DateTime startDate, DateTime endDate)
        {
            using (var db = new DbEntities())
            {
                return db.Tbl_Statistics
                    .Where(d => d.time_period == timePeriod && d.start_date >= startDate && d.end_date <= endDate)
                    .Select(d => StatisticView.ToStatisticView(d))
                    .ToHashSet();
            }
        }
    }
}
