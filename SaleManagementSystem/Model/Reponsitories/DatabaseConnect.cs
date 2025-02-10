using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementSystem.Model.Reponsitories
{
    internal class DatabaseConnect
    {
        public SqlConnection connection()
        {
            SqlConnection con = new SqlConnection(@"Data Source=LULU\SA; Initial Catalog=SaleCallCenter; User ID=sa; Password=123456;");
            return con;
        }
    }
}
