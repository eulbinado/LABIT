namespace CLMS_Student_Attendance
{
    partial class Passcode
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
            this.receivers = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.passcodeTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // receivers
            // 
            this.receivers.AutoSize = true;
            this.receivers.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receivers.Location = new System.Drawing.Point(13, 22);
            this.receivers.Name = "receivers";
            this.receivers.Size = new System.Drawing.Size(72, 18);
            this.receivers.TabIndex = 62;
            this.receivers.Text = "Passcode:";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(133, 85);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 61;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ok
            // 
            this.ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(74)))), ((int)(((byte)(173)))));
            this.ok.FlatAppearance.BorderSize = 0;
            this.ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ok.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok.ForeColor = System.Drawing.Color.White;
            this.ok.Location = new System.Drawing.Point(49, 85);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 60;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = false;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // passcodeTextbox
            // 
            this.passcodeTextbox.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passcodeTextbox.Location = new System.Drawing.Point(16, 43);
            this.passcodeTextbox.Name = "passcodeTextbox";
            this.passcodeTextbox.Size = new System.Drawing.Size(192, 23);
            this.passcodeTextbox.TabIndex = 59;
            // 
            // Passcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 128);
            this.ControlBox = false;
            this.Controls.Add(this.receivers);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.passcodeTextbox);
            this.MaximumSize = new System.Drawing.Size(239, 167);
            this.MinimumSize = new System.Drawing.Size(239, 167);
            this.Name = "Passcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passcode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label receivers;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.TextBox passcodeTextbox;
    }
}