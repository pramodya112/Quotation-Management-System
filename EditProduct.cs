using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class EditProduct : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private int productId;

        public EditProduct(int productId)
        {
            InitializeComponent();
            this.productId = productId;
            LoadSupplierIDs();  // Load Supplier IDs into ComboBox when the form is loaded
            LoadProductDetails();  // Load the product details based on the productId
        }

        // Method to load all Supplier IDs into the ComboBox
        private void LoadSupplierIDs()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT supplierID FROM suppliers"; // Query to fetch supplier IDs

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Add each supplierID to the ComboBox
                            cboSupplierID.Items.Add(reader["supplierID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier IDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to load the product details based on the ProductID
        private void LoadProductDetails()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SELECT * FROM products WHERE ProductID = {productId}"; // Query to fetch the product by ProductID

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Assuming you have textboxes like txtProductName, txtProductPrice, etc.
                            txtProductName.Text = reader["ProductName"].ToString();
                            txtProductPrice.Text = reader["Price"].ToString();
                            txtProductDescription.Text = reader["Description"].ToString();
                            txtCategory.Text = reader["Category"].ToString(); // Load the Category
                            txtQuantity.Text = reader["Quantity"].ToString(); // Load the Quantity

                            // Set the supplierID in the ComboBox (assuming the product has a supplierID column)
                            cboSupplierID.SelectedItem = reader["supplierID"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for Save button click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected supplierID from the ComboBox
                string selectedSupplierID = cboSupplierID.SelectedItem.ToString();

                // Get Category and Quantity from respective textboxes
                string category = txtCategory.Text;
                string quantity = txtQuantity.Text;

                // Validate Category and Quantity (make sure they are not empty or invalid)
                if (string.IsNullOrEmpty(category))
                {
                    MessageBox.Show("Category is required.");
                    return;
                }
                if (string.IsNullOrEmpty(quantity) || !int.TryParse(quantity, out int qty))
                {
                    MessageBox.Show("Please enter a valid quantity.");
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"UPDATE products SET ProductName = @ProductName, Price = @Price, Description = @Description, Category = @Category, Quantity = @Quantity, supplierID = @SupplierID WHERE ProductID = {productId}";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@Price", txtProductPrice.Text);
                        cmd.Parameters.AddWithValue("@Description", txtProductDescription.Text);
                        cmd.Parameters.AddWithValue("@Category", category); // Update the Category
                        cmd.Parameters.AddWithValue("@Quantity", qty); // Update the Quantity
                        cmd.Parameters.AddWithValue("@SupplierID", selectedSupplierID);  // Update the selected supplierID

                        cmd.ExecuteNonQuery(); // Execute the update query
                    }
                }
                MessageBox.Show("Product updated successfully.");
                this.Close(); // Close the form after saving
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
