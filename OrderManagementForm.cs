using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class OrderManagementForm : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        public OrderManagementForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        // Load Orders into DataGridView
        private void LoadOrders()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT OrderID, SupplierID, TechFixStaffID, OrderDate, DeliveryDate, OrderStatus FROM orders";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewOrders.DataSource = dataTable;
            }
        }

        // Update Order Status to 'Accepted'
        private void buttonUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                // Get the selected row
                int orderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["OrderID"].Value);
                string currentStatus = dataGridViewOrders.SelectedRows[0].Cells["OrderStatus"].Value.ToString();

                // Check if the order is in 'Processing' status
                if (currentStatus == "Processing")
                {
                    // Update the status to 'Accepted'
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        string updateQuery = "UPDATE orders SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID";
                        MySqlCommand cmd = new MySqlCommand(updateQuery, con);
                        cmd.Parameters.AddWithValue("@OrderStatus", "Accepted");  // New status
                        cmd.Parameters.AddWithValue("@OrderID", orderId);

                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Order status updated successfully.");
                                LoadOrders(); // Reload orders to reflect changes
                            }
                            else
                            {
                                MessageBox.Show("Failed to update order status.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating status: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Order status is not 'Processing'. Cannot update.");
                }
            }
            else
            {
                MessageBox.Show("Please select an order to update.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MenuForm homeForm = new MenuForm();
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MenuSup homeForm = new MenuSup();


            this.Hide();


            homeForm.ShowDialog();


            this.Close();
        }

        private void OrderManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
