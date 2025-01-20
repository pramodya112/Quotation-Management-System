using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class QuoteRequest : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        public QuoteRequest()
        {
            InitializeComponent();
            LoadSuppliers();
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
                    string supplierName = reader["SupplierName"].ToString();
                    int supplierId = Convert.ToInt32(reader["SupplierID"]);

                    comboBox1.Items.Add(new ComboBoxItem(supplierId, supplierName));
                    comboBox2.Items.Add(new ComboBoxItem(supplierId, supplierName));
                }
            }
        }

        private void LoadProducts1(int supplierId)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ProductID, ProductName, Category, Description, Price FROM products WHERE SupplierID = @SupplierID", con);
                adapter.SelectCommand.Parameters.AddWithValue("@SupplierID", supplierId);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; 
            }
        }

        private void LoadProducts2(int supplierId)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ProductID, ProductName, Category, Description, Price FROM products WHERE SupplierID = @SupplierID", con);
                adapter.SelectCommand.Parameters.AddWithValue("@SupplierID", supplierId);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable; 
            }
        }

        private class ComboBoxItem
        {
            public int Id { get; }
            public string Name { get; }

            public ComboBoxItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name; 
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int supplierId = ((ComboBoxItem)comboBox1.SelectedItem).Id;
            LoadProducts1(supplierId);
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int supplierId = ((ComboBoxItem)comboBox2.SelectedItem).Id;
            LoadProducts2(supplierId);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedSupplier)
            {
                CreateQuote(selectedSupplier.Id);
            }
            else
            {
                MessageBox.Show("Please select a supplier", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem is ComboBoxItem selectedSupplier)
            {
                CreateQuote(selectedSupplier.Id);
            }
            else
            {
                MessageBox.Show("Please select a supplier", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CreateQuote(int supplierId)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO quotes (SupplierID, Status, RequestedDate) VALUES (@SupplierID, @Status, @RequestedDate)", con);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                cmd.Parameters.AddWithValue("@Status", "Requested");
                cmd.Parameters.AddWithValue("@RequestedDate", DateTime.Now); 

                cmd.ExecuteNonQuery();
                MessageBox.Show("Quote requested successfully.", "Request Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            MenuForm homeForm = new MenuForm();

            
            this.Hide();

            
            homeForm.ShowDialog();

            
        }

        private void QuoteRequest_Load(object sender, EventArgs e)
        {

        }
    }
}
