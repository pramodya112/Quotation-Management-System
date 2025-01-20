using System;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class AddSupplier : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        public AddSupplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO suppliers (SupplierName, ContactEmail, Address, PhoneNumber) VALUES (@SupplierName, @ContactEmail, @Address, @PhoneNumber)", con);
                cmd.Parameters.AddWithValue("@SupplierName", txtName.Text);
                cmd.Parameters.AddWithValue("@ContactEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Supplier added successfully.", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void AddSupplier_Load(object sender, EventArgs e)
        {

        }
    }
}
