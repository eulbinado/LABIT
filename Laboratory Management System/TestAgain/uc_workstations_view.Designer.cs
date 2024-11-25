namespace Laboratory_Management_System.UserControls.AdminControls
{
    partial class uc_workstations_view
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.viewTabControl = new System.Windows.Forms.TabControl();
            this.infoPage = new System.Windows.Forms.TabPage();
            this.componentsPage = new System.Windows.Forms.TabPage();
            this.historyPage = new System.Windows.Forms.TabPage();
            this.backButton = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.viewTabControl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(60)))), ((int)(((byte)(170)))));
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Workstations - View Details";
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
            this.panel1.TabIndex = 3;
            // 
            // viewTabControl
            // 
            this.viewTabControl.Controls.Add(this.infoPage);
            this.viewTabControl.Controls.Add(this.componentsPage);
            this.viewTabControl.Controls.Add(this.historyPage);
            this.viewTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTabControl.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTabControl.Location = new System.Drawing.Point(0, 0);
            this.viewTabControl.Name = "viewTabControl";
            this.viewTabControl.Padding = new System.Drawing.Point(6, 8);
            this.viewTabControl.SelectedIndex = 0;
            this.viewTabControl.Size = new System.Drawing.Size(892, 528);
            this.viewTabControl.TabIndex = 4;
            // 
            // infoPage
            // 
            this.infoPage.Location = new System.Drawing.Point(4, 41);
            this.infoPage.Name = "infoPage";
            this.infoPage.Padding = new System.Windows.Forms.Padding(3);
            this.infoPage.Size = new System.Drawing.Size(884, 483);
            this.infoPage.TabIndex = 0;
            this.infoPage.Text = "Info";
            this.infoPage.UseVisualStyleBackColor = true;
            // 
            // componentsPage
            // 
            this.componentsPage.Location = new System.Drawing.Point(4, 41);
            this.componentsPage.Name = "componentsPage";
            this.componentsPage.Padding = new System.Windows.Forms.Padding(3);
            this.componentsPage.Size = new System.Drawing.Size(884, 483);
            this.componentsPage.TabIndex = 1;
            this.componentsPage.Text = "Components          ";
            this.componentsPage.UseVisualStyleBackColor = true;
            // 
            // historyPage
            // 
            this.historyPage.Location = new System.Drawing.Point(4, 41);
            this.historyPage.Name = "historyPage";
            this.historyPage.Padding = new System.Windows.Forms.Padding(3);
            this.historyPage.Size = new System.Drawing.Size(884, 483);
            this.historyPage.TabIndex = 2;
            this.historyPage.Text = "History         ";
            this.historyPage.UseVisualStyleBackColor = true;
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.backButton.IconColor = System.Drawing.Color.Black;
            this.backButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.backButton.IconSize = 25;
            this.backButton.Location = new System.Drawing.Point(846, 108);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 39);
            this.backButton.TabIndex = 5;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.viewTabControl);
            this.panel2.Location = new System.Drawing.Point(29, 174);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(892, 528);
            this.panel2.TabIndex = 6;
            // 
            // uc_workstations_view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.panel1);
            this.Name = "uc_workstations_view";
            this.Size = new System.Drawing.Size(955, 731);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.viewTabControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl viewTabControl;
        private System.Windows.Forms.TabPage infoPage;
        private System.Windows.Forms.TabPage componentsPage;
        private System.Windows.Forms.TabPage historyPage;
        private FontAwesome.Sharp.IconButton backButton;
        private System.Windows.Forms.Panel panel2;
    }
}
