using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class QuoteResponse : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private DataTable quoteResponseDataTable;

        public QuoteResponse()
        {
            InitializeComponent();
            LoadQuoteResponses(); // Load orders when the form is initialized
            comboBoxStatus.Items.AddRange(new string[] { "Pending", "Accepted", "Rejected" });
        }

        private void LoadQuoteResponses()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                // Query to fetch orders for the logged-in supplier
                string query = @"
                    SELECT o.OrderID, o.OrderDate, o.DeliveryDate, o.OrderStatus, 
                           oi.Quantity, oi.TotalPrice, p.ProductName
                    FROM orders o
                    JOIN orderitems oi ON o.OrderID = oi.OrderID
                    JOIN products p ON oi.ProductID = p.ProductID
                    WHERE o.SupplierID = @SupplierID";  // Filter by SupplierID

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", Login.LoggedInUserId); // Use logged-in user ID to filter

                    try
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        quoteResponseDataTable = new DataTable();
                        adapter.Fill(quoteResponseDataTable);

                        // Check if data is returned
                        if (quoteResponseDataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No orders found for this supplier.");
                        }

                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = quoteResponseDataTable;

                    }
                    catch (Exception ex)
                    {
                        // Display any errors with the SQL query or connection
                        MessageBox.Show("Error fetching orders: " + ex.Message);
                    }
                }
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to update.");
                return;
            }

            // Get the selected order ID
            int orderId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);

            // Get the new status from the ComboBox
            if (comboBoxStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            string newStatus = comboBoxStatus.SelectedItem.ToString();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                // Query to update the order status
                string updateQuery = "UPDATE orders SET OrderStatus = @Status WHERE OrderID = @OrderID";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if the update was successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Order status updated successfully!");
                        LoadQuoteResponses(); // Reload the orders after updating
                    }
                    else
                    {
                        MessageBox.Show("Error updating order status. Please try again.");
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Populate the form fields when selecting an order in the DataGridView
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string productName = selectedRow.Cells["ProductName"].Value.ToString();
                string status = selectedRow.Cells["OrderStatus"].Value.ToString();

                // Populate the TextBox and ComboBox with the selected order details
                textBoxProductName.Text = productName;
                comboBoxStatus.SelectedItem = status;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Navigate back to the menu screen
            MenuSup homeForm = new MenuSup();
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private void QuoteResponse_Load(object sender, EventArgs e)
        {
            // Optional: Any additional setup logic for the form can go here
        }

        internal void LoadOrders()
        {
            // This method seems to be a placeholder and is currently not needed since 
            // LoadQuoteResponses() already loads the orders.
        }
    }
}
