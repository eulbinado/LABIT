namespace Laboratory_Management_System.UserControls.LaboratoryInchargeControls
{
    partial class uc_asset_reports
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_asset_reports));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.approveButton = new System.Windows.Forms.Button();
            this.reportDropdown = new System.Windows.Forms.ComboBox();
            this.statusDropdown = new System.Windows.Forms.ComboBox();
            this.assetsDropdown = new System.Windows.Forms.ComboBox();
            this.searchFilter = new System.Windows.Forms.TextBox();
            this.roomsDropdown = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(29, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 61);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(60)))), ((int)(((byte)(170)))));
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Asset Reports";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(29, 183);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 521);
            this.panel2.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(896, 521);
            this.dataGridView1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(60)))), ((int)(((byte)(170)))));
            this.label3.Location = new System.Drawing.Point(26, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Reports Sheet";
            // 
            // approveButton
            // 
            this.approveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.approveButton.BackColor = System.Drawing.Color.ForestGreen;
            this.approveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("approveButton.BackgroundImage")));
            this.approveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.approveButton.FlatAppearance.BorderSize = 0;
            this.approveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.approveButton.Location = new System.Drawing.Point(890, 115);
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(35, 35);
            this.approveButton.TabIndex = 16;
            this.approveButton.UseVisualStyleBackColor = false;
            this.approveButton.Click += new System.EventHandler(this.approveButton_Click);
            // 
            // reportDropdown
            // 
            this.reportDropdown.DropDownWidth = 5;
            this.reportDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportDropdown.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportDropdown.FormattingEnabled = true;
            this.reportDropdown.Location = new System.Drawing.Point(475, 115);
            this.reportDropdown.Name = "reportDropdown";
            this.reportDropdown.Size = new System.Drawing.Size(126, 29);
            this.reportDropdown.TabIndex = 47;
            this.reportDropdown.SelectedIndexChanged += new System.EventHandler(this.reportDropdown_SelectedIndexChanged);
            // 
            // statusDropdown
            // 
            this.statusDropdown.DropDownWidth = 5;
            this.statusDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statusDropdown.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusDropdown.FormattingEnabled = true;
            this.statusDropdown.Location = new System.Drawing.Point(610, 115);
            this.statusDropdown.Name = "statusDropdown";
            this.statusDropdown.Size = new System.Drawing.Size(107, 29);
            this.statusDropdown.TabIndex = 46;
            this.statusDropdown.SelectedIndexChanged += new System.EventHandler(this.statusDropdown_SelectedIndexChanged_1);
            // 
            // assetsDropdown
            // 
            this.assetsDropdown.DropDownWidth = 5;
            this.assetsDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.assetsDropdown.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assetsDropdown.FormattingEnabled = true;
            this.assetsDropdown.Location = new System.Drawing.Point(316, 115);
            this.assetsDropdown.Name = "assetsDropdown";
            this.assetsDropdown.Size = new System.Drawing.Size(150, 29);
            this.assetsDropdown.TabIndex = 45;
            this.assetsDropdown.SelectedIndexChanged += new System.EventHandler(this.assetsDropdown_SelectedIndexChanged);
            // 
            // searchFilter
            // 
            this.searchFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchFilter.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchFilter.Location = new System.Drawing.Point(30, 115);
            this.searchFilter.Multiline = true;
            this.searchFilter.Name = "searchFilter";
            this.searchFilter.Size = new System.Drawing.Size(171, 29);
            this.searchFilter.TabIndex = 44;
            // 
            // roomsDropdown
            // 
            this.roomsDropdown.DropDownWidth = 90;
            this.roomsDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsDropdown.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomsDropdown.FormattingEnabled = true;
            this.roomsDropdown.Location = new System.Drawing.Point(210, 115);
            this.roomsDropdown.Name = "roomsDropdown";
            this.roomsDropdown.Size = new System.Drawing.Size(97, 29);
            this.roomsDropdown.TabIndex = 43;
            this.roomsDropdown.SelectedIndexChanged += new System.EventHandler(this.roomsDropdown_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(471, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 52;
            this.label5.Text = "Report Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(606, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 51;
            this.label4.Text = "Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(312, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 50;
            this.label7.Text = "Asset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 49;
            this.label1.Text = "Room";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 48;
            this.label6.Text = "Search";
            // 
            // uc_asset_reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.reportDropdown);
            this.Controls.Add(this.statusDropdown);
            this.Controls.Add(this.assetsDropdown);
            this.Controls.Add(this.searchFilter);
            this.Controls.Add(this.roomsDropdown);
            this.Controls.Add(this.approveButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uc_asset_reports";
            this.Size = new System.Drawing.Size(955, 731);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button approveButton;
        private System.Windows.Forms.ComboBox reportDropdown;
        private System.Windows.Forms.ComboBox statusDropdown;
        private System.Windows.Forms.ComboBox assetsDropdown;
        private System.Windows.Forms.TextBox searchFilter;
        private System.Windows.Forms.ComboBox roomsDropdown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
    }
}
