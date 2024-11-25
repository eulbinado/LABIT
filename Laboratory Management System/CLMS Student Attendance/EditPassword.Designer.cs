namespace CLMS_Student_Attendance
{
    partial class EditPassword
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
            this.label7 = new System.Windows.Forms.Label();
            this.newPasswordText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.oldPasswordText = new System.Windows.Forms.TextBox();
            this.confirmPasswordText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.showPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 18);
            this.label7.TabIndex = 80;
            this.label7.Text = "Old Password:";
            // 
            // newPasswordText
            // 
            this.newPasswordText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newPasswordText.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPasswordText.Location = new System.Drawing.Point(161, 70);
            this.newPasswordText.Multiline = true;
            this.newPasswordText.Name = "newPasswordText";
            this.newPasswordText.PasswordChar = '●';
            this.newPasswordText.Size = new System.Drawing.Size(139, 29);
            this.newPasswordText.TabIndex = 83;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(25, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 18);
            this.label8.TabIndex = 82;
            this.label8.Text = "New Password:";
            // 
            // oldPasswordText
            // 
            this.oldPasswordText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.oldPasswordText.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldPasswordText.Location = new System.Drawing.Point(161, 26);
            this.oldPasswordText.Multiline = true;
            this.oldPasswordText.Name = "oldPasswordText";
            this.oldPasswordText.PasswordChar = '●';
            this.oldPasswordText.Size = new System.Drawing.Size(139, 29);
            this.oldPasswordText.TabIndex = 81;
            // 
            // confirmPasswordText
            // 
            this.confirmPasswordText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.confirmPasswordText.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPasswordText.Location = new System.Drawing.Point(161, 114);
            this.confirmPasswordText.Multiline = true;
            this.confirmPasswordText.Name = "confirmPasswordText";
            this.confirmPasswordText.PasswordChar = '●';
            this.confirmPasswordText.Size = new System.Drawing.Size(139, 29);
            this.confirmPasswordText.TabIndex = 85;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 18);
            this.label1.TabIndex = 84;
            this.label1.Text = "Confirm Password:";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(225, 183);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 87;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // update
            // 
            this.update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(74)))), ((int)(((byte)(173)))));
            this.update.FlatAppearance.BorderSize = 0;
            this.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update.ForeColor = System.Drawing.Color.White;
            this.update.Location = new System.Drawing.Point(141, 183);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 86;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = false;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // showPassword
            // 
            this.showPassword.AutoSize = true;
            this.showPassword.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPassword.Location = new System.Drawing.Point(161, 149);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(111, 19);
            this.showPassword.TabIndex = 88;
            this.showPassword.Text = "Show password";
            this.showPassword.UseVisualStyleBackColor = true;
            this.showPassword.CheckedChanged += new System.EventHandler(this.showPassword_CheckedChanged);
            // 
            // EditPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 226);
            this.ControlBox = false;
            this.Controls.Add(this.showPassword);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.update);
            this.Controls.Add(this.confirmPasswordText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPasswordText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.oldPasswordText);
            this.Controls.Add(this.label7);
            this.MaximumSize = new System.Drawing.Size(341, 265);
            this.MinimumSize = new System.Drawing.Size(341, 265);
            this.Name = "EditPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Password";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox newPasswordText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox oldPasswordText;
        private System.Windows.Forms.TextBox confirmPasswordText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.CheckBox showPassword;
    }
}