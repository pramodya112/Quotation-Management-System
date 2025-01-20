using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class Inventory_Supplier : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private DataTable inventoryDataTable;
        private int selectedProductId;

        public Inventory_Supplier()
        {
            InitializeComponent();
            LoadInventoryData();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void LoadInventoryData(string productFilter = "")
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = @"
                    SELECT i.InventoryID, i.ProductID, p.ProductName, i.StockLevel, p.price, i.LastUpdated
                    FROM inventory i
                    JOIN products p ON i.ProductID = p.ProductID
                    WHERE i.SupplierID = @SupplierID";

                if (!string.IsNullOrEmpty(productFilter))
                {
                    query += " AND p.ProductID = @ProductID";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", Login.LoggedInUserId);

                    if (!string.IsNullOrEmpty(productFilter))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productFilter);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    inventoryDataTable = new DataTable();
                    adapter.Fill(inventoryDataTable);
                    dataGridView1.DataSource = inventoryDataTable;
                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string productName = selectedRow.Cells["ProductName"].Value.ToString();
                decimal productPrice = Convert.ToDecimal(selectedRow.Cells["price"].Value);

                textBox2.Text = productName;
                textBox3.Text = productPrice.ToString();
                selectedProductId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string productName = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(productName))
            {
                LoadInventoryData();
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string productQuery = "SELECT ProductID FROM products WHERE ProductName = @ProductName";
                using (MySqlCommand productCmd = new MySqlCommand(productQuery, con))
                {
                    productCmd.Parameters.AddWithValue("@ProductName", productName);
                    var productId = productCmd.ExecuteScalar();

                    if (productId != null)
                    {
                        LoadInventoryData(productId.ToString());
                    }
                    else
                    {
                        MessageBox.Show("No product found with the name: " + productName);
                    }
                }
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            string updatedProductName = textBox2.Text.Trim();
            if (decimal.TryParse(textBox3.Text.Trim(), out decimal updatedPrice))
            {
                UpdateProduct(selectedProductId, updatedProductName, updatedPrice);
            }
            else
            {
                MessageBox.Show("Invalid price entered.");
            }
        }

        private void UpdateProduct(int productId, string updatedName, decimal updatedPrice)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string updateQuery = @"
                    UPDATE products 
                    SET ProductName = @ProductName, price = @Price
                    WHERE ProductID = @ProductID AND ProductID IN 
                        (SELECT ProductID FROM inventory WHERE SupplierID = @SupplierID)";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", updatedName);
                    cmd.Parameters.AddWithValue("@Price", updatedPrice);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@SupplierID", Login.LoggedInUserId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product updated successfully!");
                        LoadInventoryData();
                    }
                    else
                    {
                        MessageBox.Show("Error updating product or no rows affected.");
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MenuSup homeForm = new MenuSup();
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private void Inventory_Supplier_Load(object sender, EventArgs e)
        {
            // Additional code for loading the form, if needed
        }
    }
}
