using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SOC
{
    public partial class UpdateSupplier : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private int supplierId;

        public UpdateSupplier(int id)
        {
            InitializeComponent();
            supplierId = id;
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM suppliers WHERE SupplierID = @SupplierID", con);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["SupplierName"].ToString(); // Supplier Name
                        txtEmail.Text = reader["ContactEmail"].ToString(); // Contact Email
                        txtAddress.Text = reader["Address"].ToString(); // Address
                        txtPhone.Text = reader["PhoneNumber"].ToString(); // Phone Number
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE suppliers SET SupplierName = @SupplierName, ContactEmail = @ContactEmail, Address = @Address, PhoneNumber = @PhoneNumber WHERE SupplierID = @SupplierID", con);
                cmd.Parameters.AddWithValue("@SupplierName", txtName.Text);
                cmd.Parameters.AddWithValue("@ContactEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text); 
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Supplier updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void UpdateSupplier_Load(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
