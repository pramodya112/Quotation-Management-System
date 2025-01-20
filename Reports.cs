using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            MenuForm homeForm = new MenuForm();

            
            this.Hide();

            
            homeForm.ShowDialog();

            
            this.Close();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }
    }
}
