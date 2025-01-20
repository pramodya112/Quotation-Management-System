using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class Inventory : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private DataTable inventoryDataTable;

        public Inventory()
        {
            InitializeComponent();
            InitializeStockAvailabilityComboBox(); 
            LoadInventoryData(); 
        }

        private void InitializeStockAvailabilityComboBox()
        {
            
            comboBox2.Items.Add("All");
            comboBox2.Items.Add("Available");
            comboBox2.SelectedIndex = 0; 
           
        }

        //private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //    string supplierName = textBox1.Text.Trim();
        //    string category = textBox2.Text.Trim();

            
        //    LoadInventoryData(supplierName, category);
        //}

        private void LoadInventoryData(string supplierNameFilter = "", string categoryFilter = "")
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                
                string query = @"
                    SELECT i.InventoryID, i.ProductID, i.SupplierID, i.StockLevel, i.LastUpdated
                    FROM inventory i
                    JOIN products p ON i.ProductID = p.ProductID
                    JOIN suppliers s ON i.SupplierID = s.SupplierID
                    WHERE (s.SupplierName LIKE @SupplierName OR @SupplierName = '')
                      AND (p.Category LIKE @Category OR @Category = '')";

                
                if (comboBox2.SelectedItem.ToString() == "Available")
                {
                    query += " AND i.StockLevel > 0"; 
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    
                    cmd.Parameters.AddWithValue("@SupplierName", $"%{supplierNameFilter}%");
                    cmd.Parameters.AddWithValue("@Category", $"%{categoryFilter}%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    inventoryDataTable = new DataTable();
                    adapter.Fill(inventoryDataTable);
                    dataGridView1.DataSource = inventoryDataTable; 
                }
            }
        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
            
        //    string supplierName = textBox1.Text.Trim();
        //    string category = textBox2.Text.Trim();

            
        //    LoadInventoryData(supplierName, category);
        //}

        private void button7_Click(object sender, EventArgs e)
        {
            
            MenuForm homeForm = new MenuForm();

            
            this.Hide();

            
            homeForm.ShowDialog();

            
            this.Close();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {

        }
    }
}
