using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using SaleManagementSystem.Model.Reponsitories;

namespace SaleManagementSystem.Model.View.Control
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {
        DatabaseConnect Myconnect = new DatabaseConnect();
        public Products()
        {
            InitializeComponent();
        }

        private void Product_Form_Loaded(object sender, RoutedEventArgs e)
        {
            Load_DataGrid(Myconnect.connection());
            LoadCategory();
        }
        private void LoadCategory()
        {
            cbb_CategoryName.Items.Clear();

            string query = "select category_name from Tbl_ProductCategories";

            try
            {
                using (SqlConnection con = Myconnect.connection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cbb_CategoryName.Items.Add(reader["category_name"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void Load_DataGrid(SqlConnection con)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = @"
           select pr.product_id,pr.product_name,ct.category_name,pr.stock,pr.unit_price from Tbl_Products pr join Tbl_ProductCategories ct on pr.category_id = ct.category_id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dg_Product.ItemsSource = dataTable.DefaultView; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
    }
}
