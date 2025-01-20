using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class Product : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public Product()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM products", con);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewProducts.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct addForm = new AddProduct();
            addForm.ShowDialog();
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells["ProductID"].Value);
                UpdateProduct updateForm = new UpdateProduct(productId);
                updateForm.ShowDialog();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells["ProductID"].Value);
                var confirmResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM products WHERE ProductID = @ProductID", con);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.ExecuteNonQuery();
                    }
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MenuForm homeForm = new MenuForm();
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }
    }
}
