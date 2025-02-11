using SaleManagementSystem.Model.Entities;
using SaleManagementSystem.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaleManagementSystem.Model.Repositories
{
    public sealed class StatisticRepository
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

        // 🔹 Lấy doanh thu theo khoảng thời gian
        public decimal GetRevenueByDate(DateTime startDate, DateTime endDate)
        {
            using (var db = new DbEntities())
            {
                return db.Tbl_Orders
                    .Where(o => o.status_id == 1 && o.created_date >= startDate && o.created_date <= endDate)
                    .Sum(o => (decimal?)o.total_amount ?? 0m);
            }
        }

        // 🔹 Lấy danh sách đơn hàng hoàn thành theo khoảng thời gian
        public List<OrderView> GetCompletedOrdersByDate(DateTime startDate, DateTime endDate)
        {
            using (var db = new DbEntities())
            {
                return db.Tbl_Orders
                    .Where(o => o.status_id == 1 && o.created_date >= startDate && o.created_date <= endDate)
                    .Select(o => new OrderView
                    {
                        OrderID = o.order_id,
                        CustomerName = o.Tbl_Customers.name,
                        CreatedBy = o.Tbl_Users.username,
                        CreatedDate = o.created_date ?? DateTime.MinValue,
                        TotalAmount = o.total_amount ?? 0m
                    })
                    .ToList();
            }
        }

        // 🔹 Lấy danh sách sản phẩm
        public List<ProductView> GetProductList()
        {
            using (var db = new DbEntities())
            {
                return db.Tbl_Products
                    .Select(p => new ProductView
                    {
                        ProductID = p.product_id,
                        Name = p.product_name,
                        Stock = p.stock ?? 0
                    })
                    .ToList();
            }
        }

        // 🔹 Lấy đơn hàng theo sản phẩm
        public List<ProductOrderView> GetOrdersByProduct(int productId, DateTime? startDate, DateTime? endDate)
        {
            using (var db = new DbEntities())
            {
                var query = db.Tbl_OrderDetails
                    .Where(od => od.product_id == productId)
                    .Select(od => new ProductOrderView
                    {
                        OrderID = (int)od.order_id,
                        Quantity = od.quantity,
                        UnitPrice = od.unit_price,
                        TotalPrice = od.quantity * od.unit_price,
                        CreatedDate = od.Tbl_Orders.created_date ?? DateTime.MinValue,
                        CreatedBy = od.Tbl_Orders.Tbl_Users.username,
                        CustomerName = od.Tbl_Orders.Tbl_Customers.name
                    });

                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(o => o.CreatedDate >= startDate.Value && o.CreatedDate <= endDate.Value);
                }

                return query.ToList();
            }
        }
    }
}
