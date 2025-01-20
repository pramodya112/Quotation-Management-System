using System;
using System.Windows.Forms;

namespace SOC
{
    public partial class MenuSup : BaseForm
    {
        // Private field to hold the reference of AddProduct form
        private AddProduct addProductForm = null;

        public MenuSup()
        {
            InitializeComponent();  // Initialize other components
        }

        // This method is used to initialize the Add Product button
        private void InitializeAddProductButton()
        {
            // Create Add Product button (button5)
            button5.Click += new EventHandler(button5_Click); // Link to the existing button5_Click event handler
            Controls.Add(button5); // Add the button to form
        }

        // Form load event to add the AddProduct button after everything is loaded
        private void MenuSup_Load(object sender, EventArgs e)
        {
            InitializeAddProductButton(); // Initialize the button when the form loads
        }

        // Method to open a new form and hide the current one
        private void OpenForm(Form newForm)
        {
            newForm.Show();
            this.Hide();
        }

        // Event handler for the Quote Response button (now redirects to OrderManagementForm)
        private void button2_Click(object sender, EventArgs e)
        {
            // Open the OrderManagementForm instead of QuoteResponse
            OpenForm(new OrderManagementForm());
        }

        // Event handler for the Inventory Supplier button
        private void button3_Click(object sender, EventArgs e)
        {
            // Open ProductListForm for the supplier
            OpenForm(new ProductListForm());  // Open ProductListForm instead of EditProduct
        }

        // Event handler for the Login button
        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm(new Login());
        }

        // Event handler for the Add Product button (button5)
        private void button5_Click(object sender, EventArgs e)
        {
            // Check if the AddProduct form is already open
            if (addProductForm == null || addProductForm.IsDisposed)
            {
                addProductForm = new AddProduct(); // Instantiate the AddProduct form
                addProductForm.Show(); // Show the AddProduct form
            }
            else
            {
                // If the form is already open, bring it to the front
                addProductForm.BringToFront();
            }
        }
    }
}
