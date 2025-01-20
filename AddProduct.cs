using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Import the MySQL namespace

namespace SOC
{
    public partial class AddProduct : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        public AddProduct()
        {
            InitializeComponent();
            LoadSupplierIDs(); // Load Supplier IDs when the form loads
        }

        // Load Supplier IDs into ComboBox
        private void LoadSupplierIDs()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SupplierID FROM suppliers"; // Query to fetch supplier IDs from the suppliers table

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cmbSupplierID.Items.Add(reader["SupplierID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier IDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click event handler for Save button
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrEmpty(txtProductName.Text) ||
                string.IsNullOrEmpty(txtCategory.Text) ||
                string.IsNullOrEmpty(txtPrice.Text) ||
                string.IsNullOrEmpty(txtQuantity.Text) ||
                string.IsNullOrEmpty(txtDescription.Text) ||
                cmbSupplierID.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try to parse price and quantity, handle potential errors
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price format. Please enter a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Invalid quantity format. Please enter a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Collect values from the form
            string productName = txtProductName.Text;
            string category = txtCategory.Text;
            string description = txtDescription.Text;
            int supplierID = Convert.ToInt32(cmbSupplierID.SelectedItem); // Get the selected SupplierID from the ComboBox

            // Here you would insert the data into your database, for now we show a success message
            try
            {
                // Example: Insert the product into your database
                InsertProductIntoDatabase(productName, category, price, quantity, description, supplierID);
                MessageBox.Show("Product saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Insert product into MySQL database
        private void InsertProductIntoDatabase(string productName, string category, decimal price, int quantity, string description, int supplierID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO products (ProductName, Category, Price, Quantity, Description, SupplierID) " +
                                   "VALUES (@ProductName, @Category, @Price, @Quantity, @Description, @SupplierID)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
