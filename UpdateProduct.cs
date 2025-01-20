using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class UpdateProduct : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private int productId;

        public UpdateProduct(int id)
        {
            InitializeComponent();
            productId = id;
            LoadProductData();
        }

        private void LoadProductData()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products WHERE ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtProductName.Text = reader["ProductName"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        txtQuantity.Text = reader["Quantity"].ToString();
                        txtDescription.Text = reader["Description"].ToString();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE products SET ProductName = @ProductName, Price = @Price, Quantity = @Quantity, Description = @Description WHERE ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@ProductID", productId);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Product updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            // Optional: Add any load event logic here
        }
    }
}
