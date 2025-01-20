using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class AddProduct : Form
    {
        private string connectionString = "Server=localhost;Port=3306;Database=SOC;Uid=root;Pwd=;";

        public AddProduct()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO products (ProductName, Category, Price, Quantity) VALUES (@ProductName, @Category, @Price, @Quantity)", con);
                cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Product added successfully.", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
