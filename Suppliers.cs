using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient; 

namespace SOC
{
    public partial class Supplier : BaseForm
    {
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public Supplier()
        {
            InitializeComponent();
            LoadSuppliers(); 
        }

        private void LoadSuppliers()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM suppliers", con);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int supplierId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplierID"].Value);

                
                UpdateSupplier updateForm = new UpdateSupplier(supplierId);
                updateForm.ShowDialog();

                
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please select a supplier to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int supplierId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplierID"].Value); 
                
                if (HasRelatedRecords(supplierId))
                {
                    MessageBox.Show("Cannot delete this supplier because there are related records in the inventory.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                
                var confirmResult = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM suppliers WHERE SupplierID = @SupplierID", con); 
                        cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                        cmd.ExecuteNonQuery();
                    }

                    
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        private bool HasRelatedRecords(int supplierId)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM inventory WHERE SupplierID = @SupplierID", con);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; 
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            AddSupplier addForm = new AddSupplier();
            addForm.ShowDialog();

            
            LoadSuppliers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MenuForm homeForm = new MenuForm();

            
            this.Hide();

            
            homeForm.ShowDialog();

            
            this.Close();

        }
    }
}
