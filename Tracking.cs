using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class Tracking : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private DataTable ordersDataTable;

        public Tracking()
        {
            InitializeComponent();
            InitializeOrderStatusComboBox(); 
            LoadOrders(); 
        }

        private void InitializeOrderStatusComboBox()
        {
            
            comboBox2.Items.Add("All");
            comboBox2.Items.Add("Processing");
            
            comboBox2.SelectedIndex = 0; 

            comboBox2.SelectedIndexChanged += ComboBoxOrderStatus_SelectedIndexChanged;
        }

        private void LoadOrders(string orderStatusFilter = "All", string orderIdFilter = "")
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT OrderID, SupplierID, TechFixStaffID, OrderDate, DeliveryDate, OrderStatus FROM orders WHERE 1=1";

                if (orderStatusFilter != "All")
                {
                    query += " AND OrderStatus = @OrderStatus";
                }

                if (!string.IsNullOrEmpty(orderIdFilter))
                {
                    query += " AND OrderID = @OrderID";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (orderStatusFilter != "All")
                    {
                        cmd.Parameters.AddWithValue("@OrderStatus", orderStatusFilter);
                    }

                    if (!string.IsNullOrEmpty(orderIdFilter))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderIdFilter);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    ordersDataTable = new DataTable();
                    adapter.Fill(ordersDataTable);
                    dataGridView1.DataSource = ordersDataTable; 
                }
            }
        }

        private void ComboBoxOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = comboBox2.SelectedItem.ToString();
            LoadOrders(selectedStatus); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orderId = textBox1.Text.Trim();
            string selectedStatus = comboBox2.SelectedItem.ToString();

            
            LoadOrders(selectedStatus, orderId);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            MenuForm homeForm = new MenuForm();

            
            this.Hide();

            
            homeForm.ShowDialog();

            
            this.Close();
        }

        private void Tracking_Load(object sender, EventArgs e)
        {

        }
    }
}
