using System;
using System.Windows.Forms;

namespace SOC
{
    public partial class MenuForm : BaseForm
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        
        private void OpenForm(Form newForm)
        {
            newForm.Show(); 
            this.Hide(); 
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            OpenForm(new QuoteRequest()); 
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            OpenForm(new Reports()); 
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            OpenForm(new Tracking()); 
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            OpenForm(new Orders()); 
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            OpenForm(new Inventory()); 
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            OpenForm(new Supplier()); 
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            OpenForm(new Login()); 
        }
    }
}
