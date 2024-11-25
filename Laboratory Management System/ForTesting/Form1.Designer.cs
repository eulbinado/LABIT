namespace ForTesting
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axMsRdpClient81 = new AxMSTSCLib.AxMsRdpClient8();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient81)).BeginInit();
            this.SuspendLayout();
            // 
            // axMsRdpClient81
            // 
            this.axMsRdpClient81.Enabled = true;
            this.axMsRdpClient81.Location = new System.Drawing.Point(12, 76);
            this.axMsRdpClient81.Name = "axMsRdpClient81";
            this.axMsRdpClient81.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMsRdpClient81.OcxState")));
            this.axMsRdpClient81.Size = new System.Drawing.Size(776, 473);
            this.axMsRdpClient81.TabIndex = 1;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.axMsRdpClient81);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient81)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSTSCLib.AxMsRdpClient8 axMsRdpClient81;
    }
}

