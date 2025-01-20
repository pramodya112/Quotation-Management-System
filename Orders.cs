using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class Orders : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        public Orders()
        {
            InitializeComponent();
            LoadSuppliers();
            LoadProducts();
        }

        private void LoadSuppliers()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT SupplierID, SupplierName FROM suppliers", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxSupplier.Items.Add(new ComboBoxItem(Convert.ToInt32(reader["SupplierID"]), reader["SupplierName"].ToString()));
                }
            }
        }

        private void LoadProducts()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT ProductID, ProductName, price FROM products", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxProduct.Items.Add(new ComboBoxItem(Convert.ToInt32(reader["ProductID"]), reader["ProductName"].ToString(), Convert.ToDecimal(reader["price"])));
                }
            }
        }

        private class ComboBoxItem
        {
            public int Id { get; }
            public string Name { get; }
            public decimal Price { get; }

            public ComboBoxItem(int id, string name, decimal price = 0)
            {
                Id = id;
                Name = name;
                Price = price;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxSupplier.SelectedItem == null || comboBoxProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select both a supplier and a product.");
                return;
            }

            int supplierId = ((ComboBoxItem)comboBoxSupplier.SelectedItem).Id;
            int productId = ((ComboBoxItem)comboBoxProduct.SelectedItem).Id;
            decimal unitPrice = ((ComboBoxItem)comboBoxProduct.SelectedItem).Price;
            int quantity = (int)numericUpDownQuantity.Value;
            string remarks = textBox4.Text.Trim();

            decimal totalPrice = unitPrice * quantity;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Insert order
                    string insertOrderQuery = @"
                        INSERT INTO orders (SupplierID, TechFixStaffID, OrderDate, DeliveryDate, OrderStatus)
                        VALUES (@SupplierID, @TechFixStaffID, @OrderDate, @DeliveryDate, @OrderStatus);
                        SELECT LAST_INSERT_ID();";

                    MySqlCommand orderCmd = new MySqlCommand(insertOrderQuery, con, transaction);
                    orderCmd.Parameters.AddWithValue("@SupplierID", supplierId);
                    orderCmd.Parameters.AddWithValue("@TechFixStaffID", Login.LoggedInUserId);
                    orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    orderCmd.Parameters.AddWithValue("@DeliveryDate", DateTime.Now.AddMonths(2));
                    orderCmd.Parameters.AddWithValue("@OrderStatus", "Processing");

                    int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());

                    // Insert order items
                    string insertOrderItemQuery = @"
                        INSERT INTO orderitems (OrderID, ProductID, Quantity, UnitPrice, TotalPrice, remarks)
                        VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @TotalPrice, @Remarks);";

                    MySqlCommand orderItemCmd = new MySqlCommand(insertOrderItemQuery, con, transaction);
                    orderItemCmd.Parameters.AddWithValue("@OrderID", orderId);
                    orderItemCmd.Parameters.AddWithValue("@ProductID", productId);
                    orderItemCmd.Parameters.AddWithValue("@Quantity", quantity);
                    orderItemCmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    orderItemCmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    orderItemCmd.Parameters.AddWithValue("@Remarks", remarks);

                    orderItemCmd.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("Order placed successfully!");

                    // Removed redirection to QuoteResponse to keep the user on the same page
                    // QuoteResponse quotationResponse = new QuoteResponse();
                    // quotationResponse.LoadOrders(); // Load the orders after placing a new one
                    // this.Hide();
                    // quotationResponse.ShowDialog();
                    // this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error placing order: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MenuForm homeForm = new MenuForm();
            this.Hide();
            homeForm.ShowDialog();
            this.Close();
        }

        private void Orders_Load(object sender, EventArgs e)
        {

        }
    }
}
