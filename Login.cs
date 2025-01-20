using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace SOC
{
    public partial class Login : BaseForm
    {
        // Database connection string
        private string connectionString = "Server=localhost;Port=3307;Database=SOC;Uid=root;Pwd=;";

        // API URLs
        private string adminApiUrl = "https://localhost:5001/api/admin/dashboard";
        private string supplierApiUrl = "https://localhost:5001/api/supplier/inventory";

        public static int LoggedInUserId { get; private set; }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (ValidateUser(username, password))
            {
                string userType = GetUserType(username);

                if (userType == "admin")
                {
                    MenuForm adminForm = new MenuForm();
                    adminForm.Show();
                }
                else if (userType == "supplier")
                {
                    MenuSup supplierForm = new MenuSup();
                    supplierForm.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            string query = "SELECT user_id FROM users WHERE username = @username AND password_hash = @password_hash";

            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password_hash", hashedPassword);

                con.Open();
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    LoggedInUserId = Convert.ToInt32(result);
                    return true;
                }

                return false;
            }
        }

        private string GetUserType(string username)
        {
            string query = "SELECT usertype FROM users WHERE username = @username";

            using (MySqlConnection con = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@username", username);

                con.Open();
                return (string)cmd.ExecuteScalar();
            }
        }

        private string HashPassword(string password)
        {
            return password;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void DarkSeaGreen(object sender, EventArgs e)
        {

        }

        private void Login_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
