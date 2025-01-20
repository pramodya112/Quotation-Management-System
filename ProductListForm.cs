using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class ProductListForm : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        // Constructor for the form
        public ProductListForm()
        {
            InitializeComponent();  // Initialize components
            LoadAllProducts();  // Load all products initially
        }

        // Load all products from the database and display them in the DataGridView
        private void LoadAllProducts(string searchTerm = "")
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductID, ProductName, Category, Price, Quantity, Description FROM products";

                    // If a search term is provided, filter products by name
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " WHERE ProductName LIKE @searchTerm";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                        }

                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        dataAdapter.Fill(dataTable);
                        dgvProducts.DataSource = dataTable; // Bind data to DataGridView

                        // Optionally, you can format the columns of the DataGridView here
                        dgvProducts.Columns["ProductID"].Visible = false; // Hide ProductID column
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler when a product is selected (double-clicked)
        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                // Get the selected ProductID from the DataGridView
                int productId = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["ProductID"].Value);

                // Open the EditProduct form and pass the selected product's ProductID
                EditProduct editProductForm = new EditProduct(productId);
                editProductForm.ShowDialog(); // Open the EditProduct form as a modal dialog
                LoadAllProducts(); // Reload products after the form closes (if any changes were made)
            }
        }

        // Search button click event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim(); // Get the text entered by the user
            LoadAllProducts(searchTerm); // Reload products based on the search term
        }

        // Event handler for form load (optional, can be used for any initialization)
        private void ProductListForm_Load(object sender, EventArgs e)
        {
            // Optional initialization code (if any)
        }

        // Event handler for the back button
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Close the current form
            this.Close();

            // Show the MenuSup form
            MenuSup menuSupForm = new MenuSup();
            menuSupForm.Show();
        }
    }
}
