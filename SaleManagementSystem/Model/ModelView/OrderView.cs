using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementSystem.Model.ModelView
{
    public class OrderView
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

