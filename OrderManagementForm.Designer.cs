using System.Windows.Forms;

namespace SOC
{
    partial class OrderManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewOrders;
        private Button buttonUpdateStatus;

        // Form cleanup
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderManagementForm));
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.buttonUpdateStatus = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(121, 134);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.RowTemplate.Height = 25;
            this.dataGridViewOrders.Size = new System.Drawing.Size(687, 254);
            this.dataGridViewOrders.TabIndex = 0;
            // 
            // buttonUpdateStatus
            // 
            this.buttonUpdateStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonUpdateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdateStatus.Location = new System.Drawing.Point(608, 88);
            this.buttonUpdateStatus.Name = "buttonUpdateStatus";
            this.buttonUpdateStatus.Size = new System.Drawing.Size(126, 40);
            this.buttonUpdateStatus.TabIndex = 1;
            this.buttonUpdateStatus.Text = "Update";
            this.buttonUpdateStatus.UseVisualStyleBackColor = false;
            this.buttonUpdateStatus.Click += new System.EventHandler(this.buttonUpdateStatus_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::SOC.Properties.Resources.Icons8_Ios7_Arrows_Back3;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(803, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 38);
            this.button1.TabIndex = 99;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(290, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(69, 64);
            this.pictureBox3.TabIndex = 100;
            this.pictureBox3.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(365, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 27);
            this.label7.TabIndex = 101;
            this.label7.Text = "Order PLATFORM";
            // 
            // OrderManagementForm
            // 
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(873, 400);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonUpdateStatus);
            this.Controls.Add(this.dataGridViewOrders);
            this.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 14.25F);
            this.Name = "OrderManagementForm";
            this.Text = "Order Management";
            this.Load += new System.EventHandler(this.OrderManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button button1;
        private PictureBox pictureBox3;
        private Label label7;
    }
}
